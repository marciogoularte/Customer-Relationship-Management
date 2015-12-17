namespace CRM.Services.Data.ViewModels.Finance.FinanceInvoice
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Finance;

    public class FinanceInvoiceViewModel
    {
        public static Expression<Func<FinanceInvoice, FinanceInvoiceViewModel>> FromFinanceInvoice
        {
            get
            {
                return fi => new FinanceInvoiceViewModel()
                {
                    Id = fi.Id,
                    NumberOfInvoice = fi.NumberOfInvoice,
                    Date = fi.Date,
                    Receiver = fi.Receiver,
                    SumWithoutDdS = fi.SumWithoutDdS,
                    SumWithDds = fi.SumWithDds
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string NumberOfInvoice { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Receiver { get; set; }

        public long SumWithoutDdS { get; set; }

        public long SumWithDds { get; set; }
    }
}
