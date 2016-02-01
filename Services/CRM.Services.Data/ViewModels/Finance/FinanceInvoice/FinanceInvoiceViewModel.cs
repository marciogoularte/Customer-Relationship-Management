using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Finance.FinanceInvoice
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Finance;

    public class FinanceInvoiceViewModel : IMapFrom<FinanceInvoice>
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Number of invoices")]
        public string NumberOfInvoice { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Receiver { get; set; }

        [Url]
        public string Preview { get; set; }

        [DisplayName("Sum without DDS")]
        public long SumWithoutDdS { get; set; }

        [DisplayName("Sum with DDS")]
        public long SumWithDds { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }
    }
}
