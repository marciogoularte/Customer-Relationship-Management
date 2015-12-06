namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Users.Discussions;

    public interface IDiscussionsServices : IService
    {
        DiscussionViewModel DiscussionInformation(int discussionId);

        List<string> ClientsDiscussionsNames();

        List<string> ProvidersDiscussionsNames();

        List<DiscussionViewModel> ReadClientsDiscussions(string searchTerm, int clientId);

        List<DiscussionViewModel> ReadProvidersDiscussions(string searchTerm, int providerId);

        DiscussionViewModel CreateDiscussion(DiscussionViewModel discussion, int? currentClientId, int? currentProviderId, string loggedUserId);

        DiscussionViewModel UpdateDiscussion(DiscussionViewModel discussion);

        DiscussionViewModel DestroyDiscussion(DiscussionViewModel discussion);

        List<DiscussionViewModel> UpcomingDiscussions();
    }
}