namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Contracts
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class ClientContractViewModel
    {
        public static Expression<Func<ClientContract, ClientContractViewModel>> FromClientContract
        {
            get
            {
                return c => new ClientContractViewModel()
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    TypeOfContract = c.TypeOfContract,
                    EndDate = c.EndDate,
                    NoticePeriod = c.NoticePeriod,
                    BillingStartDate = c.BillingStartDate,
                    BillingEndDate = c.BillingEndDate,
                    NumberOfDaysForPaymentDueDate = c.NumberOfDaysForPaymentDueDate,
                    NumberOfDaysToBeConsidered = c.NumberOfDaysToBeConsidered,
                    AcceptingReports = c.AcceptingReports,
                    GoverningLaw = c.GoverningLaw,
                    Comments = c.Comments,
                    ClientId = c.ClientId,
<<<<<<< HEAD
                    ProviderId = c.ProviderId.ToString(),
                    Tier = c.Tier
=======
                    ProviderId = c.ProviderId.ToString()
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

<<<<<<< HEAD
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
=======
        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Type of contract")]
        public string TypeOfContract { get; set; }

<<<<<<< HEAD
        [Required]
        [DisplayName("End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
=======
        [DisplayName("End date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Notice period")]
        public int NoticePeriod { get; set; }

<<<<<<< HEAD
        [UIHint("TierEditor")]
        public Tier Tier { get; set; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        [DataType(DataType.Date)]
        [DisplayName("Billing start date")]
        public DateTime? BillingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Billing end date")]
        public DateTime? BillingEndDate { get; set; }

        [DisplayName("Payment due date(number of days)")]
        public int NumberOfDaysForPaymentDueDate { get; set; }

        [DisplayName("To be considered(number of days)")]
        public int NumberOfDaysToBeConsidered { get; set; }

        [DisplayName("Accepting reports")]
<<<<<<< HEAD
        public bool AcceptingReports { get; set; }
=======
        public int AcceptingReports { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [DisplayName("Provider")]
        [UIHint("ContractProviderEditor")]
        public string ProviderId { get; set; }
    }
}