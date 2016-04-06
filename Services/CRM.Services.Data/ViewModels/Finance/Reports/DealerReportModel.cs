namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.ComponentModel;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class DealerReportModel : IMapFrom<User>
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Total number of contracts")]
        public int TotalContracts { get; set; }

        [DisplayName("Total number of contracts for the selected period")]
        public int TotalContractsForPeriod { get; set; }

        [DisplayName("Total contract value from all contracts")]
        public double TotalMonthlyFee { get; set; }

        [DisplayName("Total contract value from all contracts for the selected period")]
        public double TotalMonthlyFeeForPeriod { get; set; }
     }
}