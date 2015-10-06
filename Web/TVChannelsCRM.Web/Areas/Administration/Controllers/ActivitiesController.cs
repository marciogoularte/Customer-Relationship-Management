namespace TVChannelsCRM.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data;
<<<<<<< HEAD
    using Data.Models;
    using Web.ViewModels;
=======
    using Models;
    using Data.Models;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
    using Web.Controllers;

    [Authorize(Roles = "Admin")]
    public class ActivitiesController : BaseController
    {
        public ActivitiesController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public async Task<ActionResult> Index()
        {
            List<ActivityViewModel> lastActivities;

            if (this.User.IsInRole("Admin"))
            {
                lastActivities = await this.Data.Activities
                    .All()
                    .Select(ActivityViewModel.FromActivity)
                    .OrderByDescending(a => a.CreatedOn)
                    .ToListAsync();
            }
            else
            {
                lastActivities = await this.Data.Activities
                    .All()
                    .Select(ActivityViewModel.FromActivity)
                    .Where(a => a.TargetType != ActivityTargetType.User && a.Type != ActivityType.Restore)
                    .OrderByDescending(a => a.CreatedOn)
                    .ToListAsync();
            }

            return View(lastActivities);
        }
    }
}