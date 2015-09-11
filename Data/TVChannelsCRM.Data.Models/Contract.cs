namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Contract : DeletableEntity, IEntity
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        public int Term { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public int NoticePeriod { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BillingStartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BillingEndDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartingDateOfDueDate { get; set; }

        public int NumberOfDaysForPaymentDueDate { get; set; }

        public int NumberOfDaysToBeConsidered { get; set; }

        public int AcceptingReports { get; set; }

        public string GoverningLaw { get; set; }

        public string Comments { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
