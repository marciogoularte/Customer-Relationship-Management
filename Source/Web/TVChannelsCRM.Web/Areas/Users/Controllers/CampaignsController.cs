namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Web.Controllers;
    using System.Linq;
    using Kendo.Mvc.UI;
    using ViewModels.Campaigns;
    using System.Collections.Generic;
    using Kendo.Mvc.Extensions;
    using Data.Models;
    using System.Threading.Tasks;
    using System.Data.Entity;

    public class CampaignsController : BaseController
    {
        public CampaignsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("All");
        }

        public ActionResult GetCampaignData([DataSourceRequest]DataSourceRequest request)
        {
            var campaignsData = this.Data.Campaigns
                .All()
                .Select(c => c.Type)
                .ToList();

            return Json(campaignsData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadCampaigns([DataSourceRequest] DataSourceRequest request, string searchbox)
        {
            List<CampaignViewModel> campaigns;
            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {

                campaigns = this.Data.Campaigns
                    .All()
                    .Select(CampaignViewModel.FromCampaign)
                    .ToList();
            }
            else
            {
                campaigns = this.Data.Campaigns
                    .All()
                    .Select(CampaignViewModel.FromCampaign)
                   .Where(c => c.Type.Contains(searchbox))
                   .ToList();
            }

            return Json(campaigns.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateCampaign([DataSourceRequest]  DataSourceRequest request, CampaignViewModel campaign)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { campaign }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newCampaign = new Campaign
            {
                Type = campaign.Type,
                Activities = campaign.Activities,
                Budget = campaign.Budget,
                Start = campaign.Start,
                End = campaign.End,
                Results = campaign.Results
            };

            this.Data.Campaigns.Add(newCampaign);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newCampaign.Id.ToString(), ActivityTargetType.Campaign);
            campaign.Id = newCampaign.Id;

            return Json(new[] { campaign }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CampaignDetails(int campaignId)
        {
            var campaign = await this.Data.Campaigns
                .All()
                .Select(CampaignViewModel.FromCampaign)
                .FirstOrDefaultAsync(t => t.Id == campaignId);

            return View(campaign);
        }

        public JsonResult UpdateCampaign([DataSourceRequest] DataSourceRequest request, CampaignViewModel campaign)
        {
            var campaignFromDb = this.Data.Campaigns
                .All()
              .FirstOrDefault(c => c.Id == campaign.Id);

            if (campaign == null || !ModelState.IsValid || campaignFromDb == null)
            {
                return Json(new[] { campaign }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            campaignFromDb.Type = campaign.Type;
            campaignFromDb.Activities = campaign.Activities;
            campaignFromDb.Budget = campaign.Budget;
            campaignFromDb.Start = campaign.Start;
            campaignFromDb.End = campaign.End;
            campaignFromDb.Results = campaign.Results;

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, campaignFromDb.Id.ToString(), ActivityTargetType.Campaign);

            return Json((new[] { campaign }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyCampaign([DataSourceRequest] DataSourceRequest request, CampaignViewModel campaign)
        {
            this.Data.Campaigns.Delete(campaign.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, campaign.Id.ToString(), ActivityTargetType.Campaign);

            return Json(new[] { campaign }, JsonRequestBehavior.AllowGet);
        }
    }
}