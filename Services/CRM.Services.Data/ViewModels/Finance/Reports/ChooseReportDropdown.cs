namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel.DataAnnotations;

    public enum ChooseReportDropdown
    {
        [Display(Description = "By client")]
        ByClient = 0,

        [Display(Description = "By dealer")]
        ByDealer = 1,

        [Display(Description = "Total unpaid invoices")]
        TotalUnpaidInvoices = 2,

        [Display(Description = "Total paid invoices")]
        TotalPaidInvoices = 3,

        [Display(Description = "By TV channels")]
        ByTvChannel = 4,

        [Display(Description = "By provider")]
        ByProvider = 5,

        [Display(Description = "By month")]
        ByMonth = 6,

        [Display(Description = "By year")]
        ByYears = 7,

        [Display(Description = "By Q")]
        ByQ = 8,

        [Display(Description = "By EPG")]
        ByEpg = 9,

        [Display(Description = "By Newsletter")]
        ByNewsLetter = 10
    }
}
