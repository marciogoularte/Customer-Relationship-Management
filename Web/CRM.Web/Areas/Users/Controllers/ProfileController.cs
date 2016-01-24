namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Web.Controllers;
    using Services.Logic.Contracts.Users;
    using Services.Data.ViewModels.Users.Profile;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileServices profile;

        public ProfileController(IProfileServices profile)
        {
            this.profile = profile;
        }

        public ActionResult Index(string userId)
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var user = this.profile.Index(userId, loggedUserId);

            return user == null ? View("NoProfileAccess") : View(user);
        }

        public ActionResult CalendarScheduler(string userId)
        {
            return PartialView("_CalendarScheduler", userId);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request, string userId)
        {
            var schedulerTasks = this.profile.Read(userId);

            return Json(schedulerTasks);
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }

            var deletedTask = this.profile.Destroy(task);
            task.Id = deletedTask.Id;

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }

            var createdTask = this.profile.Create(task);
            task.Id = createdTask.Id;

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, SchedulerTaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }

            var schedulerTaskFromDb = this.profile.Update(task);
            task.Id = schedulerTaskFromDb.Id;

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        //public ActionResult UpcomingActivities()
        //{
        //    var loggedUserId = this.User.Identity.GetUserId();
        //    var upcomingActivities = this.profile.UpcomingActivities(loggedUserId);

        //    return View(upcomingActivities);
        //}
    }
}