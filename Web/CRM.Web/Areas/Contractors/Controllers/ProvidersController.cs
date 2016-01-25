using System.Collections.Generic;
using System.Web;
using CRM.Services.Data.ViewModels.Contracts.Providers;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Web.Areas.Contractors.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Common.Base;
    using Data.Models;
    using Web.Controllers;
    using Services.Logic.Contracts.Users;

    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProvidersServices providers;

        public ProvidersController(IProvidersServices providers)
        {
            this.providers = providers;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var providersNames = this.providers.GetProvidersNames();

            var vm = this.providers.Index();
            ViewData["TypeOfCompanies"] = vm;

            return View(providersNames);
        }

        [HttpGet]
        public ActionResult ProviderInformation(int providerId)
        {
            var provider = this.providers.ProviderInformation(providerId);

            if (provider == null)
            {
                return PartialView("_ProviderInformation", null);
            }

            if (string.IsNullOrEmpty(provider.TypeOfCompany))
            {
                return PartialView("_ProviderInformation", provider);
            }

            var parsedTypeOfCompany = int.Parse(provider.TypeOfCompany);
            var typeOfCompany = this.providers.GetTypeOfCompany(parsedTypeOfCompany);
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
            var readProviders = this.providers.ReadProviders(searchboxProviders);

            return Json(readProviders.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateProvider([DataSourceRequest]  DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider == null || !ModelState.IsValid)
            {
                return Json(new[] { provider }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdProvider = this.providers.CreateProvider(provider);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdProvider.Id.ToString(), ActivityTargetType.Provider, loggedUserId);

            provider.Id = createdProvider.Id;

            return Json(new[] { provider }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateProvider([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider == null || !ModelState.IsValid)
            {
                return Json((new[] { provider }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
            }

            var updatedProvider = this.providers.UpdateProvider(provider);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedProvider.Id.ToString(), ActivityTargetType.Provider, loggedUserId);

            return Json((new[] { provider }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyProvider([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            var deletedProvider = this.providers.DestroyProvider(provider);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedProvider.Id.ToString(), ActivityTargetType.Provider, loggedUserId);

            return Json(new[] { provider }, JsonRequestBehavior.AllowGet);
        }

        public string GetProviderName(int providerId)
        {
            var providerName = this.providers.GetProviderName(providerId);

            return providerName;
        }
    }
}