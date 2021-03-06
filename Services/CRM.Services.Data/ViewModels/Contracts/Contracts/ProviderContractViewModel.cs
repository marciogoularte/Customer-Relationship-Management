﻿namespace CRM.Services.Data.ViewModels.Contracts.Contracts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class ProviderContractViewModel : IMapFrom<ProviderContract>
    {
        //public static Expression<Func<ProviderContract, ProviderContractViewModel>> FromProviderContract
        //{
        //    get
        //    {
        //        return c => new ProviderContractViewModel()
        //        {
        //            Id = c.Id,
        //            StartDate = c.StartDate,
        //            TypeOfContract = c.TypeOfContract,
        //            EndDate = c.EndDate,
        //            NoticePeriod = c.NoticePeriod,
        //            BillingStartDate = c.BillingStartDate,
        //            BillingEndDate = c.BillingEndDate,
        //            NumberOfDaysForPaymentDueDate = c.NumberOfDaysForPaymentDueDate,
        //            AcceptingReports = c.AcceptingReports,
        //            GoverningLaw = c.GoverningLaw,
        //            Comments = c.Comments,
        //            ProviderId = c.ProviderId
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }
        
        [ScaffoldColumn(false)]
        [DisplayName("Type of contract")]
        public string TypeOfContract { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [Required]
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

        [DisplayName("Accepting reports")]
        public bool AcceptingReports { get; set; }

        [DisplayName("Governing law")]
        public string GoverningLaw { get; set; }

        [DisplayName("Annual indexation(in %)")]
        public int AnnualIndexation { get; set; }

        [DisplayName("Comments")]
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }
    }
}