namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Contracts
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class ProviderContractViewModel
    {
        public static Expression<Func<ProviderContract, ProviderContractViewModel>> FromProviderContract
        {
            get
            {
                return c => new ProviderContractViewModel()
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    TypeOfContract = c.TypeOfContract,
                    EndDate = c.EndDate,
                    NoticePeriod = c.NoticePeriod,
                    BillingStartDate = c.BillingStartDate,
                    BillingEndDate = c.BillingEndDate,
                    NumberOfDaysForPaymentDueDate = c.NumberOfDaysForPaymentDueDate,
<<<<<<< HEAD
=======
                    NumberOfDaysToBeConsidered = c.NumberOfDaysToBeConsidered,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                    AcceptingReports = c.AcceptingReports,
                    GoverningLaw = c.GoverningLaw,
                    Comments = c.Comments,
                    ProviderId = c.ProviderId
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Type of contract")]
        public string TypeOfContract { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Notice period")]
        public int NoticePeriod { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Billing start date")]
        public DateTime? BillingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Billing end date")]
        public DateTime? BillingEndDate { get; set; }

        [DisplayName("Payment due date(number of days)")]
        public int NumberOfDaysForPaymentDueDate { get; set; }

<<<<<<< HEAD
        [DisplayName("Accepting reports")]
        public bool AcceptingReports { get; set; }
=======
        [DisplayName("To be considered(number of days)")]
        public int NumberOfDaysToBeConsidered { get; set; }

        [DisplayName("Accepting reports")]
        public int AcceptingReports { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }
    }
}