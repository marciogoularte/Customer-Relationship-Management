namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel;

    public enum ChooseReportDropdown
    {
        [Description("By client")]
        ByClient = 0,
        [Description("By dealer")]
        ByDealer = 1,
        [Description("Total unpaid invoices")]
        TotalUnpaidInvoices = 2,
        [Description("Total paid invoices")]
        TotalPaidInvoices = 3,
        [Description("By TV channels")]
        ByTvChannel = 4,
        [Description("By provider")]
        ByProvider = 5,
        [Description("By month")]
        ByMonth = 6,
        [Description("By year")]
        ByYears = 7,
        [Description("By Q")]
        ByQ = 8,
        [Description("By EPG")]
        ByEpg = 9,
        [Description("By Newsletter")]
        ByNewsLetter = 10
    }
}
