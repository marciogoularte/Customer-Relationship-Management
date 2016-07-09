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
    using Services.Data.ViewModels.Contracts.Trds;
    
    [Authorize]
    public class TrdsController : BaseController
    {
        private readonly ITrdsServices trds;

        public TrdsController(ITrdsServices trds)
        {
            this.trds = trds;
        }

        [HttpGet]
        public ActionResult AllTrds(int clientId)
        {
            return PartialView("_ClientTrds", clientId);
        }

        public ActionResult GetTrdsData([DataSourceRequest]DataSourceRequest request)
        {
            var trdsData = this.trds.GetTrdsData();

            return Json(trdsData, JsonRequestBehavior.AllowGet);
        }

<<<<<<< HEAD
        public ActionResult ReadTrds([DataSourceRequest] DataSourceRequest request, string searchbox, int clientId, bool? showAll)
        {
            var readTrds = (showAll != null) ? (this.trds.ReadTrds(searchbox, clientId, (bool)showAll)) : (this.trds.ReadTrds(searchbox, clientId, false));
=======
        public ActionResult ReadTrds([DataSourceRequest] DataSourceRequest request, string searchbox, int clientId, bool showAll)
        {
            var readTrds = this.trds.ReadTrds(searchbox, clientId, showAll);
>>>>>>> d5b65130ac06472e570e2926b4106b53b6bd5ff6

            return Json(readTrds.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateTrd([DataSourceRequest]  DataSourceRequest request, TrdViewModel trd, string clientIdString)
        {
            if (trd == null || !ModelState.IsValid)
            {
                return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdTrd = this.trds.CreateTrd(trd, clientIdString);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdTrd.Id.ToString(), ActivityTargetType.Trd, loggedUserId);

            trd.Id = createdTrd.Id;

            return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TrdDetails(int trdId)
        {
            var trd = this.trds.TrdDetails(trdId);

            return View(trd);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateTrd([DataSourceRequest] DataSourceRequest request, TrdViewModel trd)
        {
            if (trd == null || !ModelState.IsValid)
            {
                return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedTrd = this.trds.UpdateTrd(trd);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedTrd.Id.ToString(), ActivityTargetType.Trd, loggedUserId);

            return Json((new[] { trd }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyTrd([DataSourceRequest] DataSourceRequest request, TrdViewModel trd)
        {
            var deletedTrd = this.trds.DestroyTrd(trd);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedTrd.Id.ToString(), ActivityTargetType.Trd, loggedUserId);

            return Json(new[] { trd }, JsonRequestBehavior.AllowGet);
        }
    }
}