namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Services.Logic.Contracts.Users;

    [Authorize]
    public class UserActivitiesController : Controller
    {
        private readonly IUserActivitiesServices userActivities;

        public UserActivitiesController(IUserActivitiesServices userActivities)
        {
            this.userActivities = userActivities;
        }

        public ActionResult PreviousActivities()
        {
            var loggedUserId = this.User
                .Identity
                .GetUserId();

            var activities = this.userActivities
                .GetPreviousActivities(loggedUserId);

            return View("UserActivities", activities);
        }
        
        public ActionResult UpcomingActivities()
        {
            var loggedUserId = this.User
                .Identity
                .GetUserId();

            var activities = this.userActivities
                .GetUpcomingActivities(loggedUserId);

            return View("UserActivities", activities);
        }

        public ActionResult FinishActivityProccess(int activityId)
        {
            return View(activityId);
        }

        public ActionResult FinishActivity(string activityId, string comments, string date)
        {
            var activityIdAsInt = int.Parse(activityId);
            this.userActivities.FinishActivity(activityIdAsInt, comments, date);

            return RedirectToAction("PreviousActivities");
        }

        public ActionResult FinishTask(string taskId)
        {
            var taskIdAsInt = int.Parse(taskId);
            this.userActivities.FinishTask(taskIdAsInt);

            return RedirectToAction("PreviousActivities");
        }
    }
}