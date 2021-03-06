﻿namespace CRM.Services.Data.ViewModels.Contracts.Trds
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class TrdViewModel : IMapFrom<Trd>
    {
    //    public static Expression<Func<Trd, TrdViewModel>> FromTrd
    //    {
    //        get
    //        {
    //            return t => new TrdViewModel()
    //            {
    //                Id = t.Id,
    //                Decoder = t.Decoder,
    //                Sim = t.Sim,
    //                Cas = t.Cas,
    //                Cam = t.Cam,
    //                ClientId = t.ClientId
    //            };
    //        }
    //    }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Decoder { get; set; }

        [Required]
        public string Sim { get; set; }

        [Required]
        public string Cas { get; set; }

        [Required]
        public string Cam { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }
    }
}