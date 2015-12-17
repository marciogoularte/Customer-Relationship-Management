namespace CRM.Services.Data.ViewModels.Finance.Payments
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Finance;

    public class PaymentViewModel
    {
        public static Expression<Func<Payment, PaymentViewModel>> FromPayment
        {
            get
            {
                return fi => new PaymentViewModel()
                {
                    Id = fi.Id,
                    Date = fi.Date,
                    Expense = fi.Expense,
                    Payer = fi.Payer,
                    Invoice = fi.Invoice,
                    Amount = fi.Amount
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Expense { get; set; }

        public string Payer { get; set; }

        public string Invoice { get; set; }

        public string Amount { get; set; }
    }
}
