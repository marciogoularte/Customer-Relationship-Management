namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Web.Controllers;
    using Services.Logic.Contracts.Users;

    public class FinanceController : BaseController
    {
        private readonly IFinanceServices finance;

        public FinanceController(IFinanceServices finance)
        {
            this.finance = finance;
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