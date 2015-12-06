namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Users.Clients;
    using Data.ViewModels.Users.Providers;

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