namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Discussions;

    public interface IDiscussionsServices : IService
    {
        DiscussionViewModel DiscussionInformation(int discussionId);

        List<string> ClientsDiscussionsNames();

        List<string> ProvidersDiscussionsNames();

        List<DiscussionViewModel> ReadClientsDiscussions(string searchTerm, int clientId, bool showAll);

        List<DiscussionViewModel> ReadProvidersDiscussions(string searchTerm, int providerId, bool showAll);

        DiscussionViewModel CreateDiscussion(DiscussionViewModel discussion, int? currentClientId, int? currentProviderId, string loggedUserId);

        DiscussionViewModel UpdateDiscussion(DiscussionViewModel discussion);

        DiscussionViewModel DestroyDiscussion(DiscussionViewModel discussion);

        List<DiscussionViewModel> UpcomingDiscussions();
    }
}