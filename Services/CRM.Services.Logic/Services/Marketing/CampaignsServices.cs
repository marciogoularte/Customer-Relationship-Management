namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Marketing;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Marketing.Campaigns;
    using Data.ViewModels.Contracts.Providers;
    
    public class CampaignsServices : ICampaignsServices
    {
        private ICRMData Data { get; }

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

        public CampaignPartnersViewModel CampaignPartners(int campaignId)
        {
            var campaign = this.Data.Campaigns
                .GetById(campaignId);

            var result = new CampaignPartnersViewModel
            {
                CampaignId = campaignId
            };

            foreach (var provider in campaign.Providers)
            {
                var selectedProvider = this.Data.Providers
                    .All()
                    .ProjectTo<ProviderViewModel>()
                    .FirstOrDefault(p => p.Id == provider.Id);

                result.CampaignProviders.Add(selectedProvider);
            }

            foreach (var client in campaign.Clients)
            {
                var selectedClient = this.Data.Clients
                    .All()
                    .ProjectTo<ClientViewModel>()
                    .FirstOrDefault(p => p.Id == client.Id);

                result.CampaignClients.Add(selectedClient);
            }

            var providers = this.Data.Providers
                .All()
                .ProjectTo<ProviderViewModel>()
                .ToList();

            foreach (var provider in providers)
            {
                if (result.CampaignProviders.All(p => p.Id != provider.Id))
                {
                    result.OtherProviders.Add(provider);
                }
            }

            var clients = this.Data.Clients
                .All()
                .ProjectTo<ClientViewModel>()
                .ToList();

            foreach (var client in clients)
            {
                if (result.CampaignClients.All(c => c.Id != client.Id))
                {
                    result.OtherClients.Add(client);
                }
            }

            return result;
        }

        public List<CampaignViewModel> ReadCampaigns(string searchbox, bool showAll)
        {
            List<CampaignViewModel> campaigns;

            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                if (showAll)
                {
                    campaigns = this.Data.Campaigns
                        .All()
                        .ProjectTo<CampaignViewModel>()
                        .ToList();
                }
                else
                {
                    campaigns = this.Data.Campaigns
                        .All()
                        .Where(c => c.IsVisible)
                        .ProjectTo<CampaignViewModel>()
                        .ToList();
                }
            }
            else
            {
                campaigns = this.Data.Campaigns
                    .All()
                    .ProjectTo<CampaignViewModel>()
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
                IsVisible = campaign.IsVisible,
                Results = campaign.Results
            };

            this.Data.Campaigns.Add(newCampaign);
            this.Data.Campaigns.SaveChanges();

            campaign.Id = newCampaign.Id;
            return campaign;
        }

        public CampaignViewModel CampaignInformation(int campaignId)
        {
            var campaign = this.Data.Campaigns
                .All()
                .ProjectTo<CampaignViewModel>()
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
            campaignFromDb.IsVisible = campaign.IsVisible;

            this.Data.Campaigns.SaveChanges();

            return campaign;
        }

        public CampaignViewModel DestroyCampaign(CampaignViewModel campaign)
        {
            this.Data.Campaigns.Delete(campaign.Id);
            this.Data.Campaigns.SaveChanges();

            return campaign;
        }

        public void AddOrRemoveProviderFromCampaign(int campaignId, int providerId)
        {
            var campaign = this.Data.Campaigns
                .GetById(campaignId);

            var containsIt = false;
            
            foreach (var campaignProvider in campaign.Providers.Where(campaignProvider => campaignProvider.Id == providerId))
            {
                containsIt = true;
            }

            var provider = this.Data.Providers
                .GetById(providerId);

            if (containsIt)
            {
                campaign.Providers.Remove(provider);
            }
            else
            {
                campaign.Providers.Add(provider);
            }

            this.Data.SaveChanges();
        }

        public void AddOrRemoveClientFromCampaign(int campaignId, int clientId)
        {
            var campaign = this.Data.Campaigns
                .GetById(campaignId);

            var containsIt = false;

            foreach (var campaignClient in campaign.Clients.Where(campaignClient => campaignClient.Id == clientId))
            {
                containsIt = true;
            }

            var client = this.Data.Clients
                .GetById(clientId);

            if (containsIt)
            {
                campaign.Clients.Remove(client);
            }
            else
            {
                campaign.Clients.Add(client);
            }

            this.Data.SaveChanges();
        }
    }
}
