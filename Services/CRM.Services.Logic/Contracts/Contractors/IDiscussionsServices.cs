using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Discussions;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
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