namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    public class CurrentTasksController : Controller
    {
        public ActionResult TodaysPayments()
        {
            return View();
        }
    }
}