namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Data;
    using Web.Controllers;

    public class MarketingController : BaseController
    {
        public MarketingController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public ActionResult Campaigns()
        {
           return View();
        }

        public ActionResult Partners()
        {
           return View();
        }

        public ActionResult Social()
        {
           return View();
        }
    }
}