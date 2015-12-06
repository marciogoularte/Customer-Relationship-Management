namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Users.Campaigns;

    public interface ICampaignsServices : IService
    {
        List<string> GetCampaignData();

        List<CampaignViewModel> ReadCampaigns(string searchbox);

        CampaignViewModel CreateCampaign(CampaignViewModel campaign);

        CampaignViewModel CampaignDetails(int campaignId);

        CampaignViewModel UpdateCampaign(CampaignViewModel campaign);

        CampaignViewModel DestroyCampaign(CampaignViewModel campaign);
    }
}