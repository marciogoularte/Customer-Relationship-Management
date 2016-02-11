namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Marketing.Campaigns;

    public interface ICampaignsServices : IService
    {
        List<string> GetCampaignData();

        CampaignPartnersViewModel CampaignPartners(int campaignId);

        List<CampaignViewModel> ReadCampaigns(string searchbox, bool showAll);

        CampaignViewModel CreateCampaign(CampaignViewModel campaign);

        CampaignViewModel CampaignInformation(int campaignId);

        CampaignViewModel UpdateCampaign(CampaignViewModel campaign);

        CampaignViewModel DestroyCampaign(CampaignViewModel campaign);

        void AddOrRemoveProviderFromCampaign(int campaignId, int providerId);

        void AddOrRemoveClientFromCampaign(int campaignId, int clientId);
    }
}