namespace CRM.Web.Areas.Finance.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Common.Base;
    using Data.Models;
    using Services.Logic.Contracts.Finance;
    using Services.Data.ViewModels.Finance.Frz;

    [Authorize]
    public class FrzsController : Controller
    {
        private readonly IFrzsServices frzs;

        public FrzsController(IFrzsServices frzs)
        {
            this.frzs = frzs;
        }

        [HttpGet]
        public ActionResult AllFrzs()
        {
            return View("Frzs");
        }

        [HttpGet]
        public ActionResult FrzsInformation(int frzId)
        {
            var frzInfo = this.frzs.FrzInformation(frzId);

            return View("FrzsInformation", frzInfo);
        }

        public ActionResult FrzsNames([DataSourceRequest]DataSourceRequest request)
        {
            var frzsNames = this.frzs.Index();

            return Json(frzsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadFrzs([DataSourceRequest] DataSourceRequest request, string searchTerm)
        {
            var readFrzs = this.frzs.ReadFrzs(searchTerm);

            return Json(readFrzs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult CreateFrz([DataSourceRequest]  DataSourceRequest request, FrzViewModel frzModel)
        {
            if (frzModel == null || !ModelState.IsValid)
            {
                return Json(new[] { frzModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdFrz = this.frzs.CreateFrz(frzModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdFrz.Id.ToString(), ActivityTargetType.Frz, loggedUserId);

            frzModel.Id = createdFrz.Id;

            return Json(new[] { frzModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult UpdateFrz([DataSourceRequest] DataSourceRequest request, FrzViewModel frzModel)
        {
            if (frzModel == null || !ModelState.IsValid)
            {
                return Json(new[] { frzModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedFrz = this.frzs.UpdateFrz(frzModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedFrz.Id.ToString(), ActivityTargetType.Frz, loggedUserId);

            return Json((new[] { frzModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult DestroyFrz([DataSourceRequest] DataSourceRequest request, FrzViewModel frzModel)
        {
            var deletedFrz = this.frzs.DestroyFrz(frzModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedFrz.Id.ToString(), ActivityTargetType.Frz, loggedUserId);

            return Json(new[] { frzModel }, JsonRequestBehavior.AllowGet);
        }
    }
}