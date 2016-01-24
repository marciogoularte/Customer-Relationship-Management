namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class PartnersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}