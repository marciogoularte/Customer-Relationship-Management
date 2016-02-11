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
    public class MarketingPartnersController : Controller
    {
        private readonly IMarketingPartnersServices marketingPartners;

        public MarketingPartnersController(IMarketingPartnersServices marketingPartners)
        {
            this.marketingPartners = marketingPartners;
        }

        [HttpGet]
        public ActionResult AllMarketingPartners()
        {
            return PartialView("_MarketingPartners");
        }

        [HttpGet]
        public ActionResult MarketingPartnerInformation(int marketingPartnerId)
        {
            var marketingPartner = marketingPartners.MarketingPartnerInformation(marketingPartnerId);

            return View("MarketingPartnerInformation", marketingPartner);
        }

        public ActionResult MarketingPartnersNames([DataSourceRequest]DataSourceRequest request)
        {
            var marketingPartnersNames = marketingPartners.Index();

            return Json(marketingPartnersNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadMarketingPartners([DataSourceRequest] DataSourceRequest request, string searchTerm, bool? showAll)
        {
            var readMarketingPartners = (showAll != null) ? (this.marketingPartners.ReadMarketingPartners(searchTerm, (bool)showAll)) : (this.marketingPartners.ReadMarketingPartners(searchTerm, false));

            return Json(readMarketingPartners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateMarketingPartner([DataSourceRequest]  DataSourceRequest request, MarketingPartnerViewModel marketingPartner)
        {
            if (marketingPartner == null || !ModelState.IsValid)
            {
                return Json(new[] { marketingPartner }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdMarketingPartner = marketingPartners.CreateMarketingPartner(marketingPartner);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdMarketingPartner.Id.ToString(), ActivityTargetType.MarketingPartner, loggedUserId);

            marketingPartner.Id = createdMarketingPartner.Id;

            return Json(new[] { marketingPartner }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateMarketingPartner([DataSourceRequest] DataSourceRequest request, MarketingPartnerViewModel marketingPartner)
        {
            if (marketingPartner == null || !ModelState.IsValid)
            {
                return Json(new[] { marketingPartner }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedMarketingPartner = marketingPartners.UpdateMarketingPartner(marketingPartner);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedMarketingPartner.Id.ToString(), ActivityTargetType.MarketingPartner, loggedUserId);

            return Json((new[] { marketingPartner }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyMarketingPartner([DataSourceRequest] DataSourceRequest request, MarketingPartnerViewModel marketingPartner)
        {
            var deletedMarketingPartner = marketingPartners.DestroyMarketingPartner(marketingPartner);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedMarketingPartner.Id.ToString(), ActivityTargetType.MarketingPartner, loggedUserId);

            return Json(new[] { marketingPartner }, JsonRequestBehavior.AllowGet);
        }
    }
}