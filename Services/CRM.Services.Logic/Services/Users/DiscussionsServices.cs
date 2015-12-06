namespace CRM.Services.Logic.Services.Users
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.Discussions;

    public class DiscussionsServices : IDiscussionsServices
    {
        private ICRMData Data { get; set; }

        public DiscussionsServices(ICRMData data)
        {
            this.Data = data;
        }

        public DiscussionViewModel DiscussionInformation(int discussionId)
        {
            var discussion = this.Data.Discussions
                .All()
                .Select(DiscussionViewModel.FromDiscussion)
                .FirstOrDefault(d => d.Id == discussionId);

            return discussion;
        }

        public List<string> ClientsDiscussionsNames()
        {
            var discussionsSubjects = this.Data.Discussions
                .All()
                .Where(d => d.Client != null)
                .Select(d => d.SubjectOfDiscussion)
                .ToList();

            return discussionsSubjects;
        }

        public List<string> ProvidersDiscussionsNames()
        {
            var discussionsSubjects = this.Data.Discussions
                .All()
                .Where(d => d.Provider != null)
                .Select(d => d.SubjectOfDiscussion)
                .ToList();

            return discussionsSubjects;
        }

        public List<DiscussionViewModel> ReadClientsDiscussions(string searchTerm, int clientId)
        {
            List<DiscussionViewModel> discussions;

            if (string.IsNullOrEmpty(searchTerm) || searchTerm == "")
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ClientId != null &&
                        d.ClientId == clientId)
                    .ToList();
            }
            else
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ClientId != null &&
                        d.ClientId == clientId &&
                        d.SubjectOfDiscussion.Contains(searchTerm))
                    .ToList();
            }

            return discussions;
        }

        public List<DiscussionViewModel> ReadProvidersDiscussions(string searchTerm, int providerId)
        {
            List<DiscussionViewModel> discussions;

            if (string.IsNullOrEmpty(searchTerm) || searchTerm == "")
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ProviderId != null &&
                        d.ProviderId == providerId)
                    .ToList();
            }
            else
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ProviderId != null &&
                        d.ProviderId == providerId &&
                        d.SubjectOfDiscussion.Contains(searchTerm))
                    .ToList();
            }

            return discussions;
        }

        public DiscussionViewModel CreateDiscussion(DiscussionViewModel discussion, int? currentClientId, int? currentProviderId, string loggedUserId)
        {
            var newDiscussion = new Discussion
            {
                Date = discussion.Date,
                SubjectOfDiscussion = discussion.SubjectOfDiscussion,
                Summary = discussion.Summary,
                Type = discussion.Type,
                NextDiscussionDate = discussion.NextDiscussionDate,
                NextDiscussionNote = discussion.NextDiscussionNote,
                NextDiscussionType = discussion.NextDiscussionType,
                UserId = loggedUserId,
                Comments = discussion.Comments
            };

            if (currentClientId != null)
            {
                newDiscussion.ClientId = currentClientId;
                newDiscussion.ProviderId = null;
                newDiscussion.Provider = null;
            }
            else if (currentProviderId != null)
            {
                newDiscussion.ProviderId = currentProviderId;
                newDiscussion.ClientId = null;
                newDiscussion.Client = null;
            }

            this.Data.Discussions.Add(newDiscussion);
            this.Data.SaveChanges();

            discussion.Id = newDiscussion.Id;

            return discussion;
        }

        public DiscussionViewModel UpdateDiscussion(DiscussionViewModel discussion)
        {
            var discussionFromDb = this.Data.Discussions
                .All()
                .FirstOrDefault(d => d.Id == discussion.Id);

            discussionFromDb.Date = discussion.Date;
            discussionFromDb.SubjectOfDiscussion = discussion.SubjectOfDiscussion;
            discussionFromDb.Summary = discussion.Summary;
            discussionFromDb.Type = discussion.Type;
            discussionFromDb.NextDiscussionDate = discussion.NextDiscussionDate;
            discussionFromDb.NextDiscussionNote = discussion.NextDiscussionNote;
            discussionFromDb.NextDiscussionType = discussion.NextDiscussionType;
            discussionFromDb.Comments = discussion.Comments;

            this.Data.SaveChanges();

            return discussion;
        }

        public DiscussionViewModel DestroyDiscussion(DiscussionViewModel discussion)
        {
            this.Data.Discussions.Delete(discussion.Id);
            this.Data.SaveChanges();

            return discussion;
        }

        public List<DiscussionViewModel> UpcomingDiscussions()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now.DayOfYear;

            var allDiscussions = this.Data.Discussions
                .All()
                .Select(DiscussionViewModel.FromDiscussion)
                .ToList();

            var discussions =
                (from discussion in allDiscussions
                 let isScheduledForTheDayOfLogging = (discussion.NextDiscussionDate != null && discussion.NextDiscussionDate.Value.Year.CompareTo(currentYear) == 0 && discussion.NextDiscussionDate.Value.DayOfYear.CompareTo(currentDate) == 0 && discussion.NextDiscussionDate.Value.Month.CompareTo(currentMonth) == 0)
                 let hasCommentsAndNoFutureActivities = (!string.IsNullOrEmpty(discussion.Comments) && discussion.NextDiscussionDate == null && string.IsNullOrEmpty(discussion.NextDiscussionNote) && discussion.NextDiscussionType == DiscussionType.None)
                 let isEmpty = (discussion.IsEmpty())
                 where isScheduledForTheDayOfLogging || hasCommentsAndNoFutureActivities || isEmpty
                 select discussion)
                 .ToList();

            return discussions;
        }
    }
}
