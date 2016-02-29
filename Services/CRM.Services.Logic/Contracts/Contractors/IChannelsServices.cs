namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Providers;

    public interface IChannelsServices : IService
    {
        List<string> GetChannelsNames();

        List<ChannelViewModel> ReadChannels(string searchbox, int providerId, bool showAll);

        ChannelViewModel CreateChannel(ChannelViewModel channel, int currentProviderId);

        ChannelViewModel ChannelDetails(int channelId);

        List<ClientViewModel> ViewOperators(int channelId);

        ChannelViewModel UpdateChannel(ChannelViewModel channel);

        ChannelViewModel DestroyChannel(ChannelViewModel channel);
    }
}