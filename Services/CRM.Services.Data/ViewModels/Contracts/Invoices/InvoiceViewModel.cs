using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;
using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Contracts.Invoices
{
    public class InvoiceViewModel : IMapFrom<Invoice>
    {
        //public static Expression<Func<Invoice, InvoiceViewModel>> FromInvoice
        //{
        //    get
        //    {
        //        return i => new InvoiceViewModel()
        //        {
        //            Id = i.Id,
        //            From = i.From,
        //            To = i.To,
        //            MgSubs = i.MgSubs,
        //            Cps = i.Cps,
        //            Comments = i.Comments,
        //            CorrespondencePayment = i.CorrespondencePayment,
        //            AdditionalInformation = i.AdditionalInformation,
        //            FixedMonthlyFee = i.FixedMonthlyFee,
        //            ClientContractId = i.ClientContractId
        //        };
        //    }
        //}

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

        [DisplayName("Payment")]
        [DataType(DataType.Date)]
        public DateTime CorrespondencePayment { get; set; }

        [DisplayName("Additional information")]
        public string AdditionalInformation { get; set; }

        [DisplayName("Fixed monthly fee")]
        public double FixedMonthlyFee { get; set; }

        [Required]
        [DisplayName("VAT")]
        public bool Vat { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientContractId { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }
    }
}