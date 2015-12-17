using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Providers;

namespace CRM.Services.Data.ViewModels.Contracts.Contracts
{
    public class ClientContractChannelViewModel
    {
        public ICollection<ChannelViewModel> ProviderChannels { get; set; }

        public ICollection<ChannelViewModel> ClientContractChannels { get; set; }

        public int ClientContractId { get; set; }
    }
}