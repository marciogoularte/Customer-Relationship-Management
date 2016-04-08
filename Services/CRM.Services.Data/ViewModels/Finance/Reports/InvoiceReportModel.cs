namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System;
    using System.ComponentModel;

    public class InvoiceReportModel 
    {
        // TODO: Uncomment properties
        //public string Number { get; set; }

        [DisplayName("Client name")]
        public string ClientName { get; set; }

        [DisplayName("Dealer name")]
        public string DealerName { get; set; }
        
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [DisplayName("Additional information")]
        public string AdditionalInformation { get; set; }

        [DisplayName("Total value")]
        public double TotalValue { get; set; }

        [DisplayName("Is paid")]
        public bool IsPaid { get; set; }

        //public string DateOfLastPaying { get; set; }
    }
}
