namespace CRM.Data.Models
{
    using Contracts;

    public class Activity : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public ActivityType Type { get; set; }

        public string TargetId { get; set; }

        public ActivityTargetType TargetType { get; set; }
    }
}
