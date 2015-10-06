namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class ClientContract : DeletableEntity, IEntity
    {
        private ICollection<Invoice> invoices;
<<<<<<< HEAD
        private ICollection<Channel> channels;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        public ClientContract()
        {
            this.invoices = new HashSet<Invoice>();
<<<<<<< HEAD
            this.channels = new HashSet<Channel>();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        }

        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
        public DateTime StartDate { get; set; }

        public string TypeOfContract { get; set; }

        public DateTime EndDate { get; set; }

        public int NoticePeriod { get; set; }

        public Tier Tier { get; set; }

=======
        public DateTime? StartDate { get; set; }

        public string TypeOfContract { get; set; }

        public DateTime? EndDate { get; set; }

        public int NoticePeriod { get; set; }

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public DateTime? BillingStartDate { get; set; }

        public DateTime? BillingEndDate { get; set; }

        public int NumberOfDaysForPaymentDueDate { get; set; }

        public int NumberOfDaysToBeConsidered { get; set; }

<<<<<<< HEAD
        public bool AcceptingReports { get; set; }
=======
        public int AcceptingReports { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        public string GoverningLaw { get; set; }

        public string Comments { get; set; }

        public int? ClientId { get; set; }
        
        public virtual Client Client { get; set; }
        
        public int? ProviderId { get; set; }
        
        public virtual Provider Provider { get; set; }

        public virtual ICollection<Invoice> Invoices
        {
            get { return this.invoices; }
            set { this.invoices = value; }
<<<<<<< HEAD
        }

        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        } 
    }
}