namespace CRM.Services.Data.ViewModels.Contracts.Contracts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class ClientContractViewModel : IMapFrom<ClientContract>, IHaveCustomMappings
    {
        //public static Expression<Func<ClientContract, ClientContractViewModel>> FromClientContract
        //{
        //    get
        //    {
        //        return c => new ClientContractViewModel()
        //        {
        //            Id = c.Id,
        //            StartDate = c.StartDate,
        //            TypeOfContract = c.TypeOfContract,
        //            EndDate = c.EndDate,
        //            NoticePeriod = c.NoticePeriod,
        //            BillingStartDate = c.BillingStartDate,
        //            BillingEndDate = c.BillingEndDate,
        //            NumberOfDaysForPaymentDueDate = c.NumberOfDaysForPaymentDueDate,
        //            NumberOfDaysToBeConsidered = c.NumberOfDaysToBeConsidered,
        //            AcceptingReports = c.AcceptingReports,
        //            GoverningLaw = c.GoverningLaw,
        //            Comments = c.Comments,
        //            ClientId = c.ClientId,
        //            ProviderId = c.ProviderId.ToString(),
        //            Tier = c.Tier,
        //            Frequency = c.Frequency,
        //             MonthlyFee = c.MonthlyFee
        //    };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        
        [ScaffoldColumn(false)]
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
        
        [DisplayName("Accepting reports")]
        public bool AcceptingReports { get; set; }

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [UIHint("FrequencyEditor")]
        public Frequency Frequency { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [Required]
        [DisplayName("Provider")]
        [UIHint("ContractProviderEditor")]
        public string ProviderId { get; set; }

        [Required]
        [DisplayName("Monthly fee")]
        public double MonthlyFee { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ClientContract, ClientContractViewModel>()
                .ForMember(c => c.ProviderId, opts => opts.MapFrom(c => c.ProviderId.ToString()));
        }
    }
}