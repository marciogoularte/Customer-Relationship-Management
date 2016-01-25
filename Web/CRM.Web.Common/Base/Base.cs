namespace CRM.Web.Common.Base
{
    using System;

    using Data;
    using Data.Models;

    public static class Base
    {
        public static void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType, string loggedUserId)
        {
            var data = new CRMData(new CRMDbContext());

            var activity = new Activity()
            {
                UserId = loggedUserId,
                Type = type,
                TargetId = targetId,
                TargetType = targetType,
                CreatedOn = DateTime.Now
            };

            data.Activities.Add(activity);

            data.SaveChanges();
        }

    }
}
