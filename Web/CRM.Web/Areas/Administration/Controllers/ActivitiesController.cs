namespace CRM.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    
    using Web.Controllers;
    using Services.Logic.Contracts.Administration;

    [Authorize(Roles = "Admin")]
    public class ActivitiesController : BaseController
    {
        private readonly IActivitiesServices activities;

        public ActivitiesController(IActivitiesServices activities)
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