namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel.DataAnnotations;

    public enum ChooseFinanceReportDropdown
    {
        [Display(Description = "By client")]
        ByClient = 0,

        [Display(Description = "By dealer")]
        ByDealer = 1,

        [Display(Description = "By Total unpaid and Total paid invoices")]
        ByTotalUnpaidAndTotalPaidInvoices = 2,

        [Display(Description = "By TV channels")]
        ByTvChannel = 3,

        [Display(Description = "By date")]
        ByMonthYearQ = 4
    }
}
