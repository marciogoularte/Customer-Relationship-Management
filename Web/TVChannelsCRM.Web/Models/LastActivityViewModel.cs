namespace TVChannelsCRM.Web.Models
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    public class ActivityViewModel
    {
        public static Expression<Func<Activity, ActivityViewModel>> FromActivity
        {
            get
            {
                return a => new ActivityViewModel()
                {
                    Id = a.Id,
                    User = a.User,
                    Type = a.Type,
                    TargetId = a.TargetId,
                    TargetType = a.TargetType,
                    CreatedOn = a.CreatedOn,
                    IsDeleted = a.IsDeleted
                };
            }
        }

        public int Id { get; set; }

        public virtual User User { get; set; }

        public ActivityType Type { get; set; }

        public string TargetId { get; set; }

        public ActivityTargetType TargetType { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}