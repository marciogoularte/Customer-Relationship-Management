namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class CurrentTasksController : Controller
    {
        public ActionResult TodaysPayments()
        {
            return View();
        }
    }
}