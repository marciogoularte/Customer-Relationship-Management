namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Web.Controllers;
    using Services.Logic.Contracts.Users;

    public class MarketingController : BaseController
    {
        private readonly IMarketingServices marketing;

        public MarketingController(IMarketingServices marketing)
        {
            this.marketing = marketing;
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