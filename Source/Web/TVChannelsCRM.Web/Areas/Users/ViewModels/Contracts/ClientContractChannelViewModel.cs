namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Contracts
{
    using System.Collections.Generic;

    using Providers;

    public class ClientContractChannelViewModel
    {
        public ICollection<ChannelViewModel> ProviderChannels { get; set; }

        public ICollection<ChannelViewModel> ClientContractChannels { get; set; }

        public int ClientContractId { get; set; }
    }
}