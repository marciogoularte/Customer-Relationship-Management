namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Data;
    using Web.Controllers;

    public class FinanceController : BaseController
    {
        public FinanceController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public ActionResult Invoicing()
        {
            return View();
        }

        public ActionResult Payments()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Frz()
        {
            return View();
        }
    }
}