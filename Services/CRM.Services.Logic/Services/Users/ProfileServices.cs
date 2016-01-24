namespace CRM.Services.Logic.Services.Users
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.Profile;
    using Data.ViewModels.Administration.Admin;

    public class ProfileServices : IProfileServices
    {
        private ICRMData Data { get; set; }

        public ProfileServices(ICRMData data)
        {
            this.Data = data;
        }

        public UserViewModel Index(string userId, string loggedUserId)
        {
            UserViewModel user;

            if (string.IsNullOrEmpty(userId))
            {
                user = this.Data.Users
                  .All()
                  .ProjectTo<UserViewModel>()
                  .FirstOrDefault(u => u.Id == loggedUserId);

                return user;
            }

            // Return logged user profile
            if (userId == loggedUserId)
            {
                user = this.Data.Users
                  .All()
                  .ProjectTo<UserViewModel>()
                  .FirstOrDefault(u => u.Id == userId);

                return user;
            }

            // If try to access other profile check if logged user is admin
            var adminIds = this.Data.Users
                .All()
                .Where(u => u.EnterprisePosition == EnterprisePosition.Admin)
                .Select(u => u.Id)
                .ToList();

            if (adminIds.All(adminId => adminId != loggedUserId))
            {
                return null;
            }

            user = this.Data.Users
                .All()
                .ProjectTo<UserViewModel>()
                .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public virtual IQueryable<SchedulerTaskViewModel> Read(string userId)
        {
            var schedulerTasks = this.Data.SchedulerTasks
                .All()
                .ProjectTo<SchedulerTaskViewModel>()
                .Where(s => s.UserId == userId);

            return schedulerTasks;
        }

        public virtual SchedulerTaskViewModel Destroy(SchedulerTaskViewModel task)
        {
            this.Data.SchedulerTasks.Delete(task.Id);
            this.Data.SaveChanges();

            return task;
        }

        public virtual SchedulerTaskViewModel Create(SchedulerTaskViewModel task)
        {
            var schedulerTask = new SchedulerTask()
            {
                Title = task.Title,
                Start = task.Start,
                End = task.End,
                StartTimezone = task.StartTimezone,
                EndTimezone = task.EndTimezone,
                Description = task.Description,
                IsAllDay = task.IsAllDay,
                RecurrenceRule = task.RecurrenceRule,
                RecurrenceException = task.RecurrenceException,
                RecurrenceId = task.RecurrenceId,
                UserId = task.UserId,
                IsFinished = task.IsFinished
            };

            this.Data.SchedulerTasks.Add(schedulerTask);
            this.Data.SaveChanges();

            return task;
        }

        public virtual SchedulerTaskViewModel Update(SchedulerTaskViewModel task)
        {
            var schedulerTaskFromDb = this.Data.SchedulerTasks
                .GetById(task.Id);

            schedulerTaskFromDb.Title = task.Title;
            schedulerTaskFromDb.Start = task.Start;
            schedulerTaskFromDb.End = task.End;
            schedulerTaskFromDb.StartTimezone = task.StartTimezone;
            schedulerTaskFromDb.EndTimezone = task.EndTimezone;
            schedulerTaskFromDb.Description = task.Description;
            schedulerTaskFromDb.IsAllDay = task.IsAllDay;
            schedulerTaskFromDb.RecurrenceRule = task.RecurrenceRule;
            schedulerTaskFromDb.RecurrenceException = task.RecurrenceException;
            schedulerTaskFromDb.RecurrenceId = task.RecurrenceId;
            schedulerTaskFromDb.UserId = task.UserId;
            schedulerTaskFromDb.IsFinished = task.IsFinished;

            this.Data.SaveChanges();

            return task;
        }

        //public List<SchedulerTaskViewModel> UpcomingActivities(string userId)
        //{
        //    var activities = this.Data.SchedulerTasks
        //        .All()
        //        .Where(t => 
        //        (t.UserId == userId)
        //        && (t.IsFinished == false))
        //        .ProjectTo<SchedulerTaskViewModel>()
        //        .ToList();

        //    return activities;
        //}
    }
}
