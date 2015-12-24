using System;
using System.Linq;
using CRM.Common.Extensions;
using CRM.Services.Data.ViewModels.Finance.Reports;

namespace CRM.Web.Areas.Finance.Controllers
{
    using System.Web.Mvc;

    using Services.Logic.Contracts.Finance;

    public class ReportsController : Controller
    {
        private readonly IReportsServices reports;
        
        public ReportsController(IReportsServices reports)
        {
            this.reports = reports;
        }

        public ActionResult Index()
        {
            var model = new SendReportViewModel();

            var enumData = from ChooseReportDropdown e in Enum.GetValues(typeof(ChooseReportDropdown))
                                  select new
                                  {
                                      Id = (int)e,
                                      Name = EnumExtensions.GetDescriptionOfEnum((ChooseReportDropdown)e)
                                  };

            ViewBag.EnumData = new SelectList(enumData, "Id", "Name");

            return View(model);
        }

        [HttpGet]
        public JsonResult GetClients()
        {
            var clients = this.reports.GetClients();

            return this.Json(clients, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProviders()
        {
            var providers = this.reports.GetProviders();

            return this.Json(providers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMonths()
        {
            var months = this.reports.GetMonths();

            return this.Json(months, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetYears()
        {
            var years = this.reports.GetYears();

            return this.Json(years, JsonRequestBehavior.AllowGet);
        }
    }
}