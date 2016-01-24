namespace CRM.Web.Areas.Products.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}