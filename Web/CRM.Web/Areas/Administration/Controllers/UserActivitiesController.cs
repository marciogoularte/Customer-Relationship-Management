namespace CRM.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    
    using Web.Controllers;
    using Services.Logic.Contracts.Administration;

    [Authorize(Roles = "Admin")]
    public class UserActivitiesController : BaseController
    {
        private readonly IActivitiesServices activities;

        public UserActivitiesController(IActivitiesServices activities)
        {
            this.activities = activities;
        }

        public ActionResult Index()
        {
            var lastActivities = 
                (this.User.IsInRole("Admin")) 
                ? (this.activities.GetActivitiesInAdminRole()) 
                : (this.activities.GetActivitiesInNormalRole());

            return View(lastActivities);
        }
    }
}