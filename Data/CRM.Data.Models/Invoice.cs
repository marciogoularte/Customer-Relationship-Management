namespace CRM.Data.Models
{
    using System;

    using Contracts;

    public class Invoice : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public double MgSubs { get; set; }

        public double Cps { get; set; }

        public DateTime CorrespondencePayment { get; set; }

        public string AdditionalInformation { get; set; }

        public double FixedMonthlyFee { get; set; }

        public bool Vat { get; set; }

        public string Comments { get; set; }

        public int? ClientContractId { get; set; }
        
        public virtual ClientContract ClientContract { get; set; }
        
        public bool IsVisible { get; set; }
    }
}