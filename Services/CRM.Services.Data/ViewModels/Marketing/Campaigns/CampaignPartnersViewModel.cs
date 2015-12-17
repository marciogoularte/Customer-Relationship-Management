﻿using CRM.Services.Data.ViewModels.Contracts.Clients;
using CRM.Services.Data.ViewModels.Contracts.Providers;

namespace CRM.Services.Data.ViewModels.Marketing.Campaigns
{
    using System.Collections.Generic;

    public class CampaignPartnersViewModel
    {
        public CampaignPartnersViewModel()
        {
            this.CampaignProviders = new HashSet<ProviderViewModel>();
            this.OtherProviders = new HashSet<ProviderViewModel>();
            this.CampaignClients = new HashSet<ClientViewModel>();
            this.OtherClients = new HashSet<ClientViewModel>();
        }

        public int CampaignId { get; set; }

        public ICollection<ProviderViewModel> CampaignProviders { get; set; }

        public ICollection<ProviderViewModel> OtherProviders { get; set; }

        public ICollection<ClientViewModel> CampaignClients { get; set; }

        public ICollection<ClientViewModel> OtherClients { get; set; }
    }
}
