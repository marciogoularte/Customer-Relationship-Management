namespace CRM.Web.Areas.Marketing.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

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
                               Name = EnumExtensions.GetDescriptionOfEnum((ChooseMarketingReportDropdown)e)
                           };

            ViewBag.EnumData = new SelectList(enumData, "Id", "Name");

            return View(model);
        }
    }
}