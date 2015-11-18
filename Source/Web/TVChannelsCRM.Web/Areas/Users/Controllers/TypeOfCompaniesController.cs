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
    using ViewModels.TypeOfCompanies;

    [Authorize]
    public class TypeOfCompaniesController : BaseController
    {
        public TypeOfCompaniesController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var types = this.Data.TypeOfCompanies
                .All()
                .Select(p => p.Type)
                .ToList();

            return View(types);
        }

        [HttpGet]
        public async Task<ActionResult> TypeOfCompanyInformation(int typeOfCompanyId)
        {
            var typeOfCompany = await this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .FirstOrDefaultAsync(p => p.Id == typeOfCompanyId);

            return PartialView("_TypeOfCompanyInformation", typeOfCompany);
        }

        [HttpGet]
        public async Task<ActionResult> TypeOfCompanyDetails(int typeOfCompanyId)
        {
            var typeOfCompany = await this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .FirstOrDefaultAsync(t => t.Id == typeOfCompanyId);
            
            return View(typeOfCompany);
        }

        public JsonResult ReadTypeOfCompanies([DataSourceRequest] DataSourceRequest request, string searchboxTypeOfCompany)
        {
            List<TypeOfCompanyViewModel> TypeOfCompanies;
            if (string.IsNullOrEmpty(searchboxTypeOfCompany) || searchboxTypeOfCompany == "")
            {
                TypeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .ToList();
            }
            else
            {
                TypeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .Where(p => p.Type.Contains(searchboxTypeOfCompany))
                .ToList();
            }


            return Json(TypeOfCompanies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateTypeOfCompany([DataSourceRequest]  DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            if (typeOfCompany == null || !ModelState.IsValid)
            {
                return Json(new[] { typeOfCompany }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newTypeOfCompany = new TypeOfCompany
            {
                Type = typeOfCompany.Type,
                TypeInBulgarian = typeOfCompany.TypeInBulgarian
            };

            this.Data.TypeOfCompanies.Add(newTypeOfCompany);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newTypeOfCompany.Id.ToString(), ActivityTargetType.TypeOfCompany);

            typeOfCompany.Id = newTypeOfCompany.Id;

            return Json(new[] { typeOfCompany }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTypeOfCompany([DataSourceRequest] DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            var typeOfCompanyFromDb = this.Data.TypeOfCompanies.All()
                   .FirstOrDefault(p => p.Id == typeOfCompany.Id);

            if (typeOfCompany == null || !ModelState.IsValid || typeOfCompanyFromDb == null)
            {
                return Json((new[] { typeOfCompany }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
            }

            typeOfCompanyFromDb.Type = typeOfCompany.Type;
            typeOfCompanyFromDb.TypeInBulgarian = typeOfCompany.TypeInBulgarian;

            this.Data.SaveChanges();
            this.CreateActivity(ActivityType.Edit, typeOfCompanyFromDb.Id.ToString(), ActivityTargetType.TypeOfCompany);

            return Json((new[] { typeOfCompany }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyTypeOfCompany([DataSourceRequest] DataSourceRequest request, TypeOfCompanyViewModel typeOfCompany)
        {
            this.Data.Providers.Delete(typeOfCompany.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, typeOfCompany.Id.ToString(), ActivityTargetType.TypeOfCompany);


            return Json(new[] { typeOfCompany }, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetTypeOfCompanyById(int typeOfCompanyId)
        {
            var typeOfCompany = await this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == typeOfCompanyId)
                .Select(t => t.Type.ToString())
                .FirstOrDefaultAsync();

            return typeOfCompany;
        }
    }
}