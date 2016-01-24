namespace CRM.Services.Data.ViewModels.Users.UserActivities
{
    using System;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class UserActivity : IMapFrom<Discussion>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string SubjectOfDiscussion { get; set; }

        public string Summary { get; set; }

        public DiscussionType Type { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public bool IsFinished { get; set; }
    }
}
