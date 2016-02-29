namespace CRM.Services.Logic.Services.Users
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
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

            var userSchedulerTasks = this.Data.SchedulerTasks
                .All()
                .Where(st => st.UserId == userId && st.IsFinished)
                .Select(st => new UserActivity()
                {
                    Id = st.Id,
                    IsTask = true,
                    Date = st.Start,
                    SubjectOfDiscussion = st.Title,
                    Summary = st.Description,
                    Type = DiscussionType.None,
                    UserId = st.UserId,
                    ClientId = null,
                    ProviderId = null,
                    IsFinished = st.IsFinished
                })
                .ToList();

            var result = userActivities.Union(userSchedulerTasks).OrderByDescending(x => x.Date).ToList();

            return result;
        }

        public List<UserActivity> GetUpcomingActivities(string userId)
        {
            var userActivities = this.Data.Discussions
                .All()
                .Where(d => d.UserId == userId && !d.IsFinished)
                .ProjectTo<UserActivity>()
                .ToList();

            var userSchedulerTasks = this.Data.SchedulerTasks
                .All()
                .Where(st => st.UserId == userId && !st.IsFinished)
                .Select(st => new UserActivity()
                {
                    Id = st.Id,
                    IsTask = true,
                    Date = st.Start,
                    SubjectOfDiscussion = st.Title,
                    Summary = st.Description,
                    Type = DiscussionType.None,
                    UserId = st.UserId,
                    ClientId = null,
                    ProviderId = null,
                    IsFinished = st.IsFinished
                })
                .ToList();

            var result = userActivities.Union(userSchedulerTasks).OrderBy(x => x.Date).ToList();

            return result;
        }

        public void FinishActivity(int activityId, string comments, string date)
        {
            var activity = this.Data.Discussions
                .GetById(activityId);

            if (activity == null)
            {
                return;
            }

            activity.Comments = comments;
            activity.IsFinished = true;
            activity.NextDiscussionDate = DateTime.Parse(date);

            this.Data.SaveChanges();
        }

        public void FinishTask(int taskId)
        {
            var task = this.Data.SchedulerTasks
                .GetById(taskId);

            if (task == null)
            {
                return;
            }

            task.IsFinished = true;

            this.Data.SaveChanges();
        }
    }
}
