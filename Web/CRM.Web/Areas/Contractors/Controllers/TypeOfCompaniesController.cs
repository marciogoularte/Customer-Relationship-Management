namespace CRM.Web.Areas.Contractors.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Common.Base;
    using Data.Models;
    using Web.Controllers;
    using Services.Logic.Contracts.Contractors;
    using Services.Data.ViewModels.Contracts.TypeOfCompanies;

    [Authorize]
    public class TypeOfCompaniesController : BaseController
    {
        private readonly ITypeOfCompaniesServices typeOfCompanies;

        public TypeOfCompaniesController(ITypeOfCompaniesServices typeOfCompanies)
        {
            this.typeOfCompanies = typeOfCompanies;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var types = this.typeOfCompanies.Index();

            return View(types);
        }

        [HttpGet]
        public ActionResult TypeOfCompanyInformation(int TypeOfCompany)
        {
            var typeOfCompany = this.typeOfCompanies.TypeOfCompanyInformation(TypeOfCompany);

            return PartialView("_TypeOfCompanyInformation", typeOfCompany);
        }

        [HttpGet]
        public ActionResult TypeOfCompanyDetails(int TypeOfCompany)
        {
            var typeOfCompany = this.typeOfCompanies.TypeOfCompanyDetails(TypeOfCompany);

            return View(typeOfCompany);
        }

        public JsonResult ReadTypeOfCompanies([DataSourceRequest] DataSourceRequest request, string searchboxTypeOfCompany)
        {
            var readTypeOfCompanies = this.typeOfCompanies.ReadTypeOfCompanies(searchboxTypeOfCompany);

            return Json(readTypeOfCompanies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateTypeOfCompany([DataSourceRequest]  DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            if (typeOfCompany == null || !ModelState.IsValid)
            {
                return Json(new[] { typeOfCompany }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdTypeOfCompany = this.typeOfCompanies.CreateTypeOfCompany(typeOfCompany);


            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdTypeOfCompany.Id.ToString(), ActivityTargetType.TypeOfCompany, loggedUserId);

            typeOfCompany.Id = createdTypeOfCompany.Id;

            return Json(new[] { typeOfCompany }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateTypeOfCompany([DataSourceRequest] DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            if (typeOfCompany == null || !ModelState.IsValid)
            {
                return Json((new[] { typeOfCompany }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
            }

            var updatedTypeOfCompanie = this.typeOfCompanies.UpdateTypeOfCompany(typeOfCompany);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedTypeOfCompanie.Id.ToString(), ActivityTargetType.TypeOfCompany, loggedUserId);

            return Json((new[] { typeOfCompany }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyTypeOfCompany([DataSourceRequest] DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            var deletedTypeOfCompany = this.typeOfCompanies.DestroyTypeOfCompany(typeOfCompany);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedTypeOfCompany.Id.ToString(), ActivityTargetType.TypeOfCompany, loggedUserId);

            return Json(new[] { typeOfCompany }, JsonRequestBehavior.AllowGet);
        }

        public string GetTypeOfCompanyById(int TypeOfCompany)
        {
            var typeOfCompany = this.typeOfCompanies.GetTypeOfCompanyById(TypeOfCompany);

            return typeOfCompany;
        }
    }
}