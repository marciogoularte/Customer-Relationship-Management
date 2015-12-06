namespace CRM.Services.Logic.Services.Users
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.Campaigns;

    public class CampaignsServices : ICampaignsServices
    {
        private ICRMData Data { get; set; }

        public CampaignsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetCampaignData()
        {
            var campaignsData = this.Data.Campaigns
                .All()
                .Select(c => c.Type)
                .ToList();

            return campaignsData;
        }

        public List<CampaignViewModel> ReadCampaigns(string searchbox)
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

            return campaigns;
        }

        public CampaignViewModel CreateCampaign(CampaignViewModel campaign)
        {
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
            this.Data.Campaigns.SaveChanges();

            campaign.Id = newCampaign.Id;
            return campaign;
        }

        public CampaignViewModel CampaignDetails(int campaignId)
        {
            var campaign = this.Data.Campaigns
                .All()
                .Select(CampaignViewModel.FromCampaign)
                .FirstOrDefault(t => t.Id == campaignId);

            return campaign;
        }

        public CampaignViewModel UpdateCampaign(CampaignViewModel campaign)
        {
            var campaignFromDb = this.Data.Campaigns
                .All()
              .FirstOrDefault(c => c.Id == campaign.Id);

            campaignFromDb.Type = campaign.Type;
            campaignFromDb.Activities = campaign.Activities;
            campaignFromDb.Budget = campaign.Budget;
            campaignFromDb.Start = campaign.Start;
            campaignFromDb.End = campaign.End;
            campaignFromDb.Results = campaign.Results;

            this.Data.Campaigns.SaveChanges();

            return campaign;
        }

        public CampaignViewModel DestroyCampaign(CampaignViewModel campaign)
        {
            this.Data.Campaigns.Delete(campaign.Id);
            this.Data.Campaigns.SaveChanges();

            return campaign;
        }
    }
}
