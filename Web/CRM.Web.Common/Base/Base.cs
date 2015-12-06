namespace CRM.Web.Common.Base
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    
    public static class Base
    {
        public static void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType, string loggedUserId)
        {
            var data = new CRMData(new CRMDbContext());

            // If activities are more than 200 just override the oldest one so will not have more than 200 activities
            if (data.Activities.All().Count() >= 200)
            {
                var activity = data.Activities.All().OrderBy(a => a.CreatedOn).FirstOrDefault();
                activity.UserId = loggedUserId;
                activity.Type = type;
                activity.TargetId = targetId;
                activity.TargetType = targetType;
                activity.CreatedOn = DateTime.Now;
            }
            else
            {
                var activity = new Activity()
                {
                    UserId = loggedUserId,
                    Type = type,
                    TargetId = targetId,
                    TargetType = targetType,
                    CreatedOn = DateTime.Now
                };

                data.Activities.Add(activity);
            }

            data.SaveChanges();
        }

    }
}
