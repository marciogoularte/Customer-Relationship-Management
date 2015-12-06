namespace CRM.Services.Data.ViewModels.Users.Providers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;

    public class ChannelViewModel
    {
        public static Expression<Func<Channel, ChannelViewModel>> FromChannel
        {
            get
            {
                return c => new ChannelViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ReveivingOptions = c.ReveivingOptions,
                    SatelliteData = c.SatelliteData,
                    EpgSource = c.EpgSource,
                    Website = c.Website,
                    Presentation = c.Presentation,
                    ContractTemplate = c.ContractTemplate,
                    LogoLink = c.LogoLink,
                    Comments = c.Comments,
                    ProviderId = c.ProviderId,
                    ClientId = c.ClientId,
                    ClientContractId = c.ClientContractId
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Channel name")]
        public string Name { get; set; }

        [DisplayName("Reveiving options")]
        public string ReveivingOptions { get; set; }

        [Required]
        [UIHint("TextAreaEditor")]
        [DisplayName("Satellite data")]
        public string SatelliteData { get; set; }

        [Required]
        [DisplayName("EPG-SOURCE")]
        [Url(ErrorMessage = "Channel Epg Source is not valid website")]
        public string EpgSource { get; set; }

        [Required]
        [DisplayName("Website")]
        [Url(ErrorMessage = "Channel website is not valid website")]
        public string Website { get; set; }

        [Required]
        [Url(ErrorMessage = "Channel presentation is not valid website")]
        public string Presentation { get; set; }

        [Required]
        [DisplayName("Contract Template")]
        [Url(ErrorMessage = "Channel contract template is not valid website")]
        public string ContractTemplate { get; set; }

        [Required]
        [DisplayName("Logo Link")]
        [Url(ErrorMessage = "Channel logo link is not valid website")]
        public string LogoLink { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientContractId { get; set; }
    }
}