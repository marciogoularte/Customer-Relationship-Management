namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Profile;
    using Administration.ViewModels.Admin;

    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(ITVChannelsCRMData data)
            : base(data)
        {
        }
        
        public ActionResult Index(string userId)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            // If userId is null return logged user profile
            UserViewModel user;
            if (string.IsNullOrEmpty(userId))
            {
                user = this.Data.Users
                  .All()
                  .Select(UserViewModel.FromUser)
                  .FirstOrDefault(u => u.Id == loggedUserId);

                return View(user);
            }

            // Return logged user profile
            if (userId == loggedUserId)
            {
                user = this.Data.Users
                  .All()
                  .Select(UserViewModel.FromUser)
                  .FirstOrDefault(u => u.Id == userId);

                return View(user);
            }

            // If try to access other profile check if logged user is admin
            var adminIds = this.Data.Users
                .All()
                .Where(u => u.EnterprisePosition == EnterprisePosition.Admin)
                .Select(u => u.Id)
                .ToList();

            if (adminIds.All(adminId => adminId != loggedUserId))
            {
                return View("NoProfileAccess");
            }

            user = this.Data.Users
                .All()
                .Select(UserViewModel.FromUser)
                .FirstOrDefault(u => u.Id == userId);

            return View(user);
        }

        public ActionResult CalendarScheduler(string userId)
        {
            return PartialView("_CalendarScheduler", model: userId);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request, string userId)
        {
            var schedulerTasks = this.Data.SchedulerTasks
                .All()
                .Select(SchedulerTaskViewModel.FromSchedulerTask)
                .Where(s => s.UserId == userId)
                .ToDataSourceResult(request);

            return Json(schedulerTasks);
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                this.Data.SchedulerTasks.Delete(task.Id);
                this.Data.SaveChanges();
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (ModelState.IsValid)
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
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (ModelState.IsValid)
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
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }
    }
}