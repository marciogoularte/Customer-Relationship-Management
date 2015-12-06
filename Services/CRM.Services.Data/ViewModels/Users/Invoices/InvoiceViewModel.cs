namespace CRM.Services.Data.ViewModels.Users.Invoices
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;
   
    using CRM.Data.Models;

    public class InvoiceViewModel
    {
        public static Expression<Func<Invoice, InvoiceViewModel>> FromInvoice
        {
            get
            {
                return i => new InvoiceViewModel()
                {
                    Id = i.Id,
                    From = i.From,
                    To = i.To,
                    MgSubs = i.MgSubs,
                    Cps = i.Cps,
                    Comments = i.Comments,
                    ClientContractId = i.ClientContractId
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [Required]
        public double MgSubs { get; set; }

        [Required]
        public double Cps { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientContractId { get; set; }
    }
}