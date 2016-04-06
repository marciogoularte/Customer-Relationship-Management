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
                    SetResponse(byClient);
                    break;
                case 1:
                    var byDealer = financeReports.ByDealer(from, to);
                    SetResponse(byDealer);
                    break;
                    //case 2:
                    //    result = financeReports.ByInvoices(from, to);
                    //    break;
                    //case 3:
                    //    result = financeReports.ByTvChannels(from, to);
                    //    break;
                    //case 4:
                    //    result = financeReports.ByDate(from, to);
                    //    break;
            }

            return RedirectToAction("Index");
        }

        private void SetResponse<T>(IEnumerable<T> result)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            GenerateExcel(result, Response.Output);
            Response.End();
        }

        private void GenerateExcel<T>(IEnumerable<T> data, TextWriter output)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (var item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }
    }
}