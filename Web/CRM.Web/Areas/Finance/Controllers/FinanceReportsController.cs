namespace CRM.Web.Areas.Finance.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CRM.Common.Extensions;
    using Services.Logic.Contracts.Finance;
    using Services.Data.ViewModels.Finance.Reports;
    
    [Authorize]
    public class FinanceReportsController : Controller
    {
        private readonly IFinanceReportsServices financeReports;
        
        public FinanceReportsController(IFinanceReportsServices financeReports)
        {
            this.financeReports = financeReports;
        }

        public ActionResult Index()
        {
            var model = new SendFinanceReportViewModel();

            var enumData = from ChooseFinanceReportDropdown e in Enum.GetValues(typeof(ChooseFinanceReportDropdown))
                                  select new
                                  {
                                      Id = (int)e,
                                      Name = EnumExtensions.GetDescriptionOfEnum((ChooseFinanceReportDropdown)e)
                                  };

            ViewBag.EnumData = new SelectList(enumData, "Id", "Name");

            return View(model);
        }

        [HttpGet]
        public JsonResult GetClients()
        {
            var clients = this.financeReports.GetClients();

            return this.Json(clients, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProviders()
        {
            var providers = this.financeReports.GetProviders();

            return this.Json(providers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMonths()
        {
            var months = this.financeReports.GetMonths();

            return this.Json(months, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetYears()
        {
            var years = this.financeReports.GetYears();

            return this.Json(years, JsonRequestBehavior.AllowGet);
        }
    }
}