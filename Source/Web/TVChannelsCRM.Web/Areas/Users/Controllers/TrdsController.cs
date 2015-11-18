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
    using ViewModels.Trds;
    using Web.Controllers;

    public class TrdsController : BaseController
    {
        public TrdsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllTrds(int clientId)
        {
            return PartialView("_ClientTrds", clientId);
        }

        public ActionResult GetTrdsData([DataSourceRequest]DataSourceRequest request)
        {
            var trdsData = this.Data.Trds
                .All()
                .Select(c => c.Decoder.ToString())
                .ToList();

            return Json(trdsData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadTrds([DataSourceRequest] DataSourceRequest request, string searchbox, int clientId)
        {
            List<TrdViewModel> trds;
            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                trds = this.Data.Trds
                    .All()
                    .Select(TrdViewModel.FromTrd)
                    .Where(i => i.ClientId == clientId)
                    .ToList();
            }
            else
            {
                trds = this.Data.Trds
                   .All()
                    .Select(TrdViewModel.FromTrd)
                   .Where(i => i.ClientId == clientId && i.Decoder.Contains(searchbox))
                   .ToList();
            }

            return Json(trds.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateTrd([DataSourceRequest]  DataSourceRequest request, TrdViewModel trd, string clientIdString)
        {
            var clientId = int.Parse(clientIdString);
            if (trd == null || !ModelState.IsValid)
            {
                return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newTrd = new Trd
            {
                Decoder = trd.Decoder,
                Sim = trd.Sim,
                Cas = trd.Cas,
                Cam = trd.Cam,
                ClientId = clientId
            };

            this.Data.Trds.Add(newTrd);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newTrd.Id.ToString(), ActivityTargetType.Trd);
            trd.Id = newTrd.Id;

            return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> TrdDetails(int trdId)
        {
            var trd = await this.Data.Trds
                .All()
                .Select(TrdViewModel.FromTrd)
                .FirstOrDefaultAsync(t => t.Id == trdId);

            return View(trd);
        }

        public JsonResult UpdateTrd([DataSourceRequest] DataSourceRequest request, TrdViewModel trd)
        {
            var trdFromDb = this.Data.Trds
                .All()
              .FirstOrDefault(t => t.Id == trd.Id);

            if (trd == null || !ModelState.IsValid || trdFromDb == null)
            {
                return Json(new[] { trd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            trdFromDb.Decoder = trd.Decoder;
            trdFromDb.Sim = trd.Sim;
            trdFromDb.Cas = trd.Cas;
            trdFromDb.Cam = trd.Cam;

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, trdFromDb.Id.ToString(), ActivityTargetType.Trd);

            return Json((new[] { trd }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyTrd([DataSourceRequest] DataSourceRequest request, TrdViewModel trd)
        {
            this.Data.Trds.Delete(trd.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, trd.Id.ToString(), ActivityTargetType.Trd);

            return Json(new[] { trd }, JsonRequestBehavior.AllowGet);
        }
    }
}