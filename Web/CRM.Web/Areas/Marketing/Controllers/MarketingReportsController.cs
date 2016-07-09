namespace CRM.Web.Areas.Marketing.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using CRM.Common.Extensions;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Reports;

    public class MarketingReportsController : Controller
    {
        private readonly IMarketingReportsServices marketingReports;

        public MarketingReportsController(IMarketingReportsServices marketingReports)
        {
            this.marketingReports = marketingReports;
        }

        public ActionResult Index()
        {
            var model = new SendMarketingReportViewModel();

            var enumData = from ChooseMarketingReportDropdown e in Enum.GetValues(typeof(ChooseMarketingReportDropdown))
                           select new
                           {
                               Id = (int)e,
                               Name = EnumExtensions.GetDescriptionOfEnum(e)
                           };

            ViewBag.EnumData = new SelectList(enumData, "Id", "Name");

            return View(model);
        }

        public ActionResult GenerateReport(int chooseReportType, DateTime from, DateTime to)
        {
            if (chooseReportType == 1)
            {
                var byEpg = marketingReports.ByEpg(from, to);
                SetResponse(byEpg, $"By EPG - {from} - {to}", true);
            }
            else if (chooseReportType == 2)
            {
                var byNewsletter = marketingReports.ByNewsLetter(from, to);
                SetResponse(byNewsletter, $"By Newsletter - {from} - {to}", false);
            }

            return RedirectToAction("Index");
        }

        private void SetResponse<T>(IEnumerable<T> result, string fileName, bool isClient)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            result.GenerateExcel(Response.Output);

            Response.End();
        }
    }
}