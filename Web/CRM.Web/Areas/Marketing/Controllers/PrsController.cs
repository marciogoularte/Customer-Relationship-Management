namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Partners;

    [Authorize]
    public class PrsController : Controller
    {
        private readonly IPrsServices prs;

        public PrsController(IPrsServices prs)
        {
            this.prs = prs;
        }

        [HttpGet]
        public ActionResult AllPrs()
        {
            return PartialView("_Prs");
        }

        [HttpGet]
        public ActionResult PrsInformation(int prId)
        {
            var prInfo = prs.PrInformation(prId);

            return View("PrsInformation", prInfo);
        }

        public ActionResult PrsNames([DataSourceRequest]DataSourceRequest request)
        {
            var prsNames = prs.Index();

            return Json(prsNames, JsonRequestBehavior.AllowGet);
        }

<<<<<<< HEAD
        public JsonResult ReadPrs([DataSourceRequest] DataSourceRequest request, string searchTerm, bool? showAll)
        {
            var readPrs = (showAll != null) ? (this.prs.ReadPrs(searchTerm, (bool)showAll)) : (this.prs.ReadPrs(searchTerm, false));
=======
        public JsonResult ReadPrs([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readPrs = prs.ReadPrs(searchTerm, showAll);
>>>>>>> d5b65130ac06472e570e2926b4106b53b6bd5ff6

            return Json(readPrs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreatePr([DataSourceRequest]  DataSourceRequest request, PrViewModel prModel)
        {
            if (prModel == null || !ModelState.IsValid)
            {
                return Json(new[] { prModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdPr = this.prs.CreatePr(prModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdPr.Id.ToString(), ActivityTargetType.Pr, loggedUserId);

            prModel.Id = createdPr.Id;

            return Json(new[] { prModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdatePr([DataSourceRequest] DataSourceRequest request, PrViewModel prModel)
        {
            if (prModel == null || !ModelState.IsValid)
            {
                return Json(new[] { prModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedPr = prs.UpdatePr(prModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedPr.Id.ToString(), ActivityTargetType.Pr, loggedUserId);

            return Json((new[] { prModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyPr([DataSourceRequest] DataSourceRequest request, PrViewModel prModel)
        {
            var deletedPr = prs.DestroyPr(prModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedPr.Id.ToString(), ActivityTargetType.Pr, loggedUserId);

            return Json(new[] { prModel }, JsonRequestBehavior.AllowGet);
        }
    }
}