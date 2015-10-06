<<<<<<< HEAD
﻿using TVChannelsCRM.Web.Areas.Users.ViewModels.Contracts;
using TVChannelsCRM.Web.Areas.Users.ViewModels.TypeOfCompanies;

namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
=======
﻿namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
<<<<<<< HEAD
=======
    using Microsoft.AspNet.Identity;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

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
<<<<<<< HEAD
            
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
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

            return View(providersNames);
        }

        [HttpGet]
<<<<<<< HEAD
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
=======
        public async Task<ActionResult> ProviderInformation(int providerId)
        {
            var provider = await this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefaultAsync(p => p.Id == providerId);
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

            return PartialView("_ProviderInformation", provider);
        }

        [HttpGet]
        public ActionResult ProviderDetails(int providerId)
        {
            return View(providerId);
        }

        public JsonResult ReadProviders([DataSourceRequest] DataSourceRequest request, string searchboxProviders)
        {
<<<<<<< HEAD
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



=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
                TypeId = provider.TypeId,
                Uic = provider.Uic,
                Vat = provider.Vat,
=======
                Type = provider.Type,
                Eik = provider.Eik,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
                ContractTemplate = provider.ContractTemplate,
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                Channels = new List<Channel>(),
                Contracts = new List<ProviderContract>(),
                Discussions = new List<Discussion>()
            };

<<<<<<< HEAD
            if (string.IsNullOrEmpty(provider.LogoLink) || provider.LogoLink == "")
            {
                newProvider.LogoLink = "#";
            }
            else
            {
                newProvider.LogoLink = provider.LogoLink;
            }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
            providerFromDb.TypeId = provider.TypeId;
            providerFromDb.Uic = provider.Uic;
            providerFromDb.Vat = provider.Vat;
=======
            providerFromDb.Type = provider.Type;
            providerFromDb.Eik = provider.Eik;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
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
=======
            providerFromDb.Comments = provider.Comments + "\n";
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

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

<<<<<<< HEAD
        public string GetProviderName(int providerId)
        {
            var providerName = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefault(p => p.Id == providerId)
                .Name;

            return providerName;
=======
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
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        }
    }
}