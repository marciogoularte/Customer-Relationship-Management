namespace CRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class ClientContract : DeletableEntity, IEntity
    {
        private ICollection<Invoice> invoices;
        private ICollection<Channel> channels;

        public ClientContract()
        {
            this.invoices = new HashSet<Invoice>();
            this.channels = new HashSet<Channel>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public string TypeOfContract { get; set; }

        public DateTime EndDate { get; set; }

        public int NoticePeriod { get; set; }

        public Tier Tier { get; set; }

        public DateTime? BillingStartDate { get; set; }

        public DateTime? BillingEndDate { get; set; }

        public int NumberOfDaysForPaymentDueDate { get; set; }

        public bool AcceptingReports { get; set; }

        public string GoverningLaw { get; set; }

        public string Comments { get; set; }

        public int? ClientId { get; set; }
        
        public virtual Client Client { get; set; }
        
        public int? ProviderId { get; set; }
        
        public virtual Provider Provider { get; set; }
        
        public Frequency Frequency { get; set; }

        public double MonthlyFee { get; set; }
        
        public bool IsVisible { get; set; }

        public virtual ICollection<Invoice> Invoices
        {
            get { return this.invoices; }
            set { this.invoices = value; }
        }

        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        } 
    }
}