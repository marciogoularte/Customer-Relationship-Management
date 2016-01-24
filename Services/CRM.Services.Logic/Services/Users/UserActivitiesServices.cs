namespace CRM.Services.Logic.Services.Users
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using Contracts.Users;
    using Data.ViewModels.Users.UserActivities;

    public class UserActivitiesServices : IUserActivitiesServices
    {
        private ICRMData Data { get; set; }

        public UserActivitiesServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<UserActivity> GetPreviousActivities(string userId)
        {
            var userActivities = this.Data.Discussions
                .All()
                .Where(d => d.UserId == userId && d.IsFinished)
                .ProjectTo<UserActivity>()
                .ToList();

            return userActivities;
        }

        public List<UserActivity> GetUpcomingActivities(string userId)
        {
            var userActivities = this.Data.Discussions
                .All()
                .Where(d => d.UserId == userId && !d.IsFinished)
                .ProjectTo<UserActivity>()
                .ToList();

            return userActivities;
        }

        public void FinishActivity(int activityId, string comments, string date)
        {
            var activity = this.Data.Discussions
                .GetById(activityId);

            activity.Comments = comments;
            activity.IsFinished = true;
            activity.NextDiscussionDate = DateTime.Parse(date);

            this.Data.SaveChanges();
        }
    }
}
