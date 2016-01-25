namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Campaigns;

    [Authorize]
    public class CampaignsController : Controller
    {
        private readonly ICampaignsServices campaigns;

        public CampaignsController(ICampaignsServices campaigns)
        {
            this.campaigns = campaigns;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("All");
        }

        public ActionResult CampaignDetails(int campaignId)
        {
            return View(campaignId);
        }

        public ActionResult CampaignPartners(int campaignId)
        {
            var campaignPartners = this.campaigns.CampaignPartners(campaignId);

            return PartialView(campaignPartners);
        }

        public ActionResult GetCampaignData([DataSourceRequest]DataSourceRequest request)
        {
            var campaignsData = this.campaigns.GetCampaignData();

            return Json(campaignsData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadCampaigns([DataSourceRequest] DataSourceRequest request, string searchbox)
        {
            var readCampaigns = this.campaigns.ReadCampaigns(searchbox);

            return Json(readCampaigns.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateCampaign([DataSourceRequest]  DataSourceRequest request, CampaignViewModel campaign)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { campaign }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var model = this.campaigns.CreateCampaign(campaign);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, model.Id.ToString(), ActivityTargetType.Campaign, loggedUserId);

            return Json(new[] { campaign }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CampaignInformation(int campaignId)
        {
            var campaign = this.campaigns.CampaignInformation(campaignId);

            return PartialView(campaign);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateCampaign([DataSourceRequest] DataSourceRequest request, CampaignViewModel campaign)
        {
            var updatedCompaign = this.campaigns.UpdateCampaign(campaign);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedCompaign.Id.ToString(), ActivityTargetType.Campaign, loggedUserId);

            return Json((new[] { campaign }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyCampaign([DataSourceRequest] DataSourceRequest request, CampaignViewModel campaign)
        {
            var deletedCampaign = this.campaigns.DestroyCampaign(campaign);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedCampaign.Id.ToString(), ActivityTargetType.Campaign, loggedUserId);

            return Json(new[] { campaign }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public void AddOrRemoveProviderFromCampaign(int campaignId, int providerId)
        {
            this.campaigns.AddOrRemoveProviderFromCampaign(campaignId, providerId);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public void AddOrRemoveClientFromCampaign(int campaignId, int clientId)
        {
            this.campaigns.AddOrRemoveClientFromCampaign(campaignId, clientId);
        }
    }
}