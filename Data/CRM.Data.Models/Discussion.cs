namespace CRM.Data.Models
{
    using System;

    using Contracts;

    public class Discussion : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string SubjectOfDiscussion { get; set; }

        public string Summary { get; set; }
        
        public DiscussionType Type { get; set; }

        public DateTime? NextDiscussionDate { get; set; }

        public string NextDiscussionNote { get; set; }

        public DiscussionType NextDiscussionType { get; set; }
        
        public string Comments { get; set; }

        public bool IsFinished { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
