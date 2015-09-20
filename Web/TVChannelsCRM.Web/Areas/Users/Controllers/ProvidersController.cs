namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

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

            return View(providersNames);
        }

        [HttpGet]
        public async Task<ActionResult> ProviderInformation(int providerId)
        {
            var provider = await this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefaultAsync(p => p.Id == providerId);

            return PartialView("_ProviderInformation", provider);
        }

        [HttpGet]
        public ActionResult ProviderDetails(int providerId)
        {
            return View(providerId);
        }

        public JsonResult ReadProviders([DataSourceRequest] DataSourceRequest request, string searchboxProviders)
        {
            List<ProviderViewModel> providers;
            if (searchboxProviders == "")
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
                Type = provider.Type,
                Eik = provider.Eik,
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
                Comments = provider.Comments + "\n"
            };

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
            providerFromDb.Type = provider.Type;
            providerFromDb.Eik = provider.Eik;
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
            providerFromDb.Comments = provider.Comments + "\n";

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

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        private void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            // If activities are more than 200 just override the oldest one so will not have more than 200 activities
            if (this.Data.Activities.All().Count() >= 200)
            {
                var activity = this.Data.Activities.All().OrderBy(a => a.CreatedOn).FirstOrDefault();
                activity.UserId = loggedUserId;
                activity.Type = type;
                activity.TargetId = targetId;
                activity.TargetType = targetType;
                activity.CreatedOn = DateTime.Now;
            }
            else
            {
                var activity = new Activity()
                {
                    UserId = loggedUserId,
                    Type = type,
                    TargetId = targetId,
                    TargetType = targetType
                };

                this.Data.Activities.Add(activity);
            }

            this.Data.SaveChanges();
        }
    }
}