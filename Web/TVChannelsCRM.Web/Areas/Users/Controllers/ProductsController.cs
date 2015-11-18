namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Data;
    using Web.Controllers;

    public class ProductsController : BaseController
    {
        public ProductsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}