namespace CRM.Web.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("UpcomingDiscussions", "Discussions", new { area = "Users" });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GeneratePdf()
        {
            return new Rotativa.ViewAsPdf("SampleContract") { FileName = "TestViewAsPdf.pdf" };
        }
    }
}