namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel;

    public class DateReportModel
    {
        [DisplayName("Total profit for period")]
        public double TotalProfitForPeriod { get; set; }

        [DisplayName("Total paid")]
        public int TotalPaid { get; set; }

        [DisplayName("Total unpaid")]
        public int TotalUnpaid { get; set; }
    }
}
