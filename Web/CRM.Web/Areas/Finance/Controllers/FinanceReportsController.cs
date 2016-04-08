using System.ComponentModel;

namespace CRM.Web.Areas.Finance.Controllers
{
    using System;
    using System.IO;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;
    using System.Collections.Generic;

    using FastMember;

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

        public ActionResult GenerateReport(int chooseReportType, DateTime from, DateTime to)
        {
            switch (chooseReportType)
            {
                case 0:
                    var byClient = financeReports.ByClient(from, to);
                    SetResponse(byClient, $"By client - {from} - {to}", true);
                    break;
                case 1:
                    var byDealer = financeReports.ByDealer(from, to);
                    SetResponse(byDealer, $"By dealer - {from} - {to}", false);
                    break;
                case 2:
                    var byInvoice = financeReports.ByInvoices(from, to);
                    SetResponse(byInvoice, $"By total unpaid and total paid invoices - {from} - {to}", false);
                    break;
                case 3:
                    var byTvChannels = financeReports.ByTvChannels(from, to);
                    SetResponse(byTvChannels, $"By TV Channels - {from} - {to}", false);
                    break;
                    //case 4:
                    //    result = financeReports.ByDate(from, to, false);
                    //    break;
            }

            return RedirectToAction("Index");
        }

        private void SetResponse<T>(IEnumerable<T> result, string fileName, bool isClient)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            if (isClient)
            {
                result.WriteHtmlTable(Response.Output);
            }
            else
            {
                result.GenerateExcel(Response.Output);
            }

            Response.End();
        }
    }
}