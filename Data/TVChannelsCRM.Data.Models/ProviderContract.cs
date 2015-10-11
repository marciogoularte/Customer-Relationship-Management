namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class ProviderContract : DeletableEntity, IEntity
    {
        private ICollection<Invoice> invoices;

        public ProviderContract()
        {
            this.invoices = new HashSet<Invoice>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime? StartDate { get; set; }

        public string TypeOfContract { get; set; }

        public DateTime? EndDate { get; set; }

        public int NoticePeriod { get; set; }

        public DateTime? BillingStartDate { get; set; }

        public DateTime? BillingEndDate { get; set; }

        public int NumberOfDaysForPaymentDueDate { get; set; }

        public bool AcceptingReports { get; set; }

        public string GoverningLaw { get; set; }

        public string Comments { get; set; }

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual ICollection<Invoice> Invoices
        {
            get { return this.invoices; }
            set { this.invoices = value; }
        } 
    }
}
