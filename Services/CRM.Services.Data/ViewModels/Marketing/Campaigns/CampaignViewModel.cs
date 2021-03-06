﻿namespace CRM.Services.Data.ViewModels.Marketing.Campaigns
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class CampaignViewModel : IMapFrom<Campaign>
    {
        //public static Expression<Func<Campaign, CampaignViewModel>> FromCampaign
        //{
        //    get
        //    {
        //        return c => new CampaignViewModel
        //        {
        //            Id = c.Id,
        //            Type = c.Type,
        //            Start = c.Start,
        //            End = c.End,
        //            Activities = c.Activities,
        //            Budget = c.Budget,
        //            Results = c.Results
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public string Activities { get; set; }

        public double Budget { get; set; }

        public string Results { get; set; }

        public bool IsVisible { get; set; }
    }
}
