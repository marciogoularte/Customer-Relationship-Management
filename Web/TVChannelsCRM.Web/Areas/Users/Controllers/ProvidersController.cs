using TVChannelsCRM.Web.Areas.Users.ViewModels.Contracts;
using TVChannelsCRM.Web.Areas.Users.ViewModels.TypeOfCompanies;

namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Providers;

    [Authorize]
    public class ProvidersController : BaseController
    {
        public ProvidersController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var providersNames = this.Data.Providers
                .All()
                .Select(p => p.Name)
                .ToList();
            
            var typeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .ToList();

            var vm = typeOfCompanies
                .Select(typeOfCompany => new DatabaseDataDropdownViewModel
                        {
                            Text = typeOfCompany.Type,
                            Value = typeOfCompany.Id
                        })
                .ToList();

            ViewData["TypeOfCompanies"] = vm;

            return View(providersNames);
        }

        [HttpGet]
        public ActionResult ProviderInformation(int providerId)
        {
            var provider = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefault(p => p.Id == providerId);

            if (provider == null)
            {
                return PartialView("_ProviderInformation", null);
            }

            if (string.IsNullOrEmpty(provider.TypeId))
            {
                return PartialView("_ProviderInformation", provider);
            }

            var parsedTypeOfCompany = int.Parse(provider.TypeId);
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == parsedTypeOfCompany)
                .Select(t => t.Type.ToString())
                .FirstOrDefault();
            ViewBag.TypeOfCompany = typeOfCompany;

            return PartialView("_ProviderInformation", provider);
        }

        [HttpGet]
        public ActionResult ProviderDetails(int providerId)
        {
            return View(providerId);
        }

        public JsonResult ReadProviders([DataSourceRequest] DataSourceRequest request, string searchboxProviders)
        {
            // var typeOfCompanies = this.Data.TypeOfCompanies
            //     .All()
            //     .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
            //     .ToList();
            // 
            // var vm = typeOfCompanies
            //     .Select(typeOfCompany => new DatabaseDataDropdownViewModel
            //     {
            //         Text = typeOfCompany.Type,
            //         Value = typeOfCompany.Id
            //     })
            //     .ToList();
            // 
            // ViewData["TypeOfCompanies"] = typeOfCompanies;



            List<ProviderViewModel> providers;
             if (string.IsNullOrEmpty(searchboxProviders) || searchboxProviders == "")
            {
                providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .ToList();
            }
            else
            {
                providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .Where(p => p.Name.Contains(searchboxProviders))
                .ToList();
            }


            return Json(providers.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateProvider([DataSourceRequest]  DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider == null || !ModelState.IsValid)
            {
                return Json(new[] { provider }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newProvider = new Provider()
            {
                Name = provider.Name,
                TypeId = provider.TypeId,
                Uic = provider.Uic,
                Vat = provider.Vat,
                BankAccount = provider.BankAccount,
                ResidenceAndAddress = provider.ResidenceAndAddress,
                NetworkPage = provider.NetworkPage,
                ContactPerson = provider.ContactPerson,
                PhoneNumber = provider.PhoneNumber,
                Email = provider.Email,
                Address = provider.Address,
                Term = provider.Term,
                Cps = provider.Cps,
                Commission = provider.Commission,
                Comments = provider.Comments + "\n",
                ContractTemplate = provider.ContractTemplate,
                Channels = new List<Channel>(),
                Contracts = new List<ProviderContract>(),
                Discussions = new List<Discussion>()
            };

            if (string.IsNullOrEmpty(provider.LogoLink) || provider.LogoLink == "")
            {
                newProvider.LogoLink = "#";
            }
            else
            {
                newProvider.LogoLink = provider.LogoLink;
            }

            this.Data.Providers.Add(newProvider);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newProvider.Id.ToString(), ActivityTargetType.Provider);

            provider.Id = newProvider.Id;

            return Json(new[] { provider }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProvider([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            var providerFromDb = this.Data.Providers.All()
                   .FirstOrDefault(p => p.Id == provider.Id);

            if (provider == null || !ModelState.IsValid || providerFromDb == null)
            {
                return Json((new[] { provider }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
            }

            providerFromDb.Name = provider.Name;
            providerFromDb.TypeId = provider.TypeId;
            providerFromDb.Uic = provider.Uic;
            providerFromDb.Vat = provider.Vat;
            providerFromDb.BankAccount = provider.BankAccount;
            providerFromDb.ResidenceAndAddress = provider.ResidenceAndAddress;
            providerFromDb.NetworkPage = provider.NetworkPage;
            providerFromDb.ContactPerson = provider.ContactPerson;
            providerFromDb.PhoneNumber = provider.PhoneNumber;
            providerFromDb.Email = provider.Email;
            providerFromDb.Address = provider.Address;
            providerFromDb.Term = provider.Term;
            providerFromDb.Cps = provider.Cps;
            providerFromDb.Commission = provider.Commission;
            providerFromDb.LogoLink = provider.LogoLink;
            providerFromDb.Comments = provider.Comments + "\n";
            providerFromDb.ContractTemplate = provider.ContractTemplate;

            if (string.IsNullOrEmpty(provider.LogoLink) || provider.LogoLink == "")
            {
                providerFromDb.LogoLink = "#";
            }
            else
            {
                providerFromDb.LogoLink = provider.LogoLink;
            }

            this.Data.SaveChanges();
            this.CreateActivity(ActivityType.Edit, providerFromDb.Id.ToString(), ActivityTargetType.Provider);

            return Json((new[] { provider }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyProvider([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            var providerIdToInt = int.Parse(provider.Id.ToString());
            this.Data.Providers.Delete(providerIdToInt);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, provider.Id.ToString(), ActivityTargetType.Provider);


            return Json(new[] { provider }, JsonRequestBehavior.AllowGet);
        }

        public string GetProviderName(int providerId)
        {
            var providerName = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefault(p => p.Id == providerId)
                .Name;

            return providerName;
        }
    }
}