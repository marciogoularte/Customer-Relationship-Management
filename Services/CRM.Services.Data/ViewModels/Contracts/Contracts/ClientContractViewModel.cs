using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;

namespace CRM.Services.Data.ViewModels.Contracts.Contracts
{
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
                    ProviderId = c.ProviderId.ToString(),
                    Tier = c.Tier,
                    Frequency = c.Frequency,
                     MonthlyFee = c.MonthlyFee
            };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("Type of contract")]
        public string TypeOfContract { get; set; }

        [Required]
        [DisplayName("End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayName("Notice period")]
        public int NoticePeriod { get; set; }

        [UIHint("TierEditor")]
        public Tier Tier { get; set; }

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
        public bool AcceptingReports { get; set; }

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        public Frequency Frequency { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [DisplayName("Provider")]
        [UIHint("ContractProviderEditor")]
        public string ProviderId { get; set; }

        [Required]
        [DisplayName("Monthly fee")]
        public double MonthlyFee { get; set; }
    }
}