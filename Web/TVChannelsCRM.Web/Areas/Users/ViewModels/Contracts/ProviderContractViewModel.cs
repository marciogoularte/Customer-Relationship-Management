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
                    NumberOfDaysToBeConsidered = c.NumberOfDaysToBeConsidered,
                    AcceptingReports = c.AcceptingReports,
                    GoverningLaw = c.GoverningLaw,
                    Comments = c.Comments,
                    Provider = c.Provider
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayName("Type of contract")]
        public string TypeOfContract { get; set; }

        [DisplayName("End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        [DisplayName("Notice period")]
        public int NoticePeriod { get; set; }

        [DisplayName("Billing start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BillingStartDate { get; set; }

        [DisplayName("Billing end date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BillingEndDate { get; set; }

        [DisplayName("Payment due date(number of days)")]
        public int NumberOfDaysForPaymentDueDate { get; set; }

        [DisplayName("To be considered(number of days)")]
        public int NumberOfDaysToBeConsidered { get; set; }

        [DisplayName("Accepting reports")]
        public int AcceptingReports { get; set; }

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public virtual Provider Provider { get; set; }
    }
}