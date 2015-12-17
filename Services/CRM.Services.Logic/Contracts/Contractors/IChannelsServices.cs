using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Clients;
using CRM.Services.Data.ViewModels.Contracts.Providers;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface IChannelsServices : IService
    {
        List<string> GetChannelsNames();

        List<ChannelViewModel> ReadChannels(string searchbox, int providerId);

        ChannelViewModel CreateChannel(ChannelViewModel channel, int currentProviderId);

        ChannelViewModel ChannelDetails(int channelId);

        List<ClientViewModel> ViewOperators(int channelId);

        ChannelViewModel UpdateChannel(ChannelViewModel channel);

        ChannelViewModel DestroyChannel(ChannelViewModel channel);
    }
}