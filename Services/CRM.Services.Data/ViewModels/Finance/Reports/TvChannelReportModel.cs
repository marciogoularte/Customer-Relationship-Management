namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel;

    public class TvChannelReportModel
    {
        [DisplayName("Channel name")]
        public string ChannelName { get; set; }

        [DisplayName("Provider name")]
        public string ProviderName { get; set; }

        [DisplayName("Total paid")]
        public int TotalPaid { get; set; }

        [DisplayName("Total unpaid")]
        public int TotalUnpaid { get; set; }

        [DisplayName("Total revenues for the selected period")]
        public double TotalRevenuesForSelectedPeriod { get; set; }
    }
}
