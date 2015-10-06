namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Providers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

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
<<<<<<< HEAD
                    LogoLink = c.LogoLink,
                    Comments = c.Comments,
                    ProviderId = c.ProviderId,
                    ClientId = c.ClientId,
                    ClientContractId = c.ClientContractId
=======
                    Comments = c.Comments,
                    ProviderId = c.ProviderId,
                    ClientId = c.ClientId
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Channel name")]
        public string Name { get; set; }

        [DisplayName("Reveiving options")]
        public string ReveivingOptions { get; set; }

        [UIHint("TextAreaEditor")]
        [DisplayName("Satellite data")]
        public string SatelliteData { get; set; }

        [DisplayName("EPG-SOURCE")]
<<<<<<< HEAD
        [Url(ErrorMessage = "Channel Epg Source is not valid website")]
        public string EpgSource { get; set; }

        [DisplayName("Website")]
        [Url(ErrorMessage = "Channel website is not valid website")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Channel presentation is not valid website")]
        public string Presentation { get; set; }

        [DisplayName("Contract Template")]
        [Url(ErrorMessage = "Channel contract template is not valid website")]
        public string ContractTemplate { get; set; }

        [DisplayName("Logo Link")]
        [Url(ErrorMessage = "Channel logo link is not valid website")]
        public string LogoLink { get; set; }

=======
        public string EpgSource { get; set; }

        [DisplayName("Website")]
        [Url(ErrorMessage = "Provider website is not valid")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Presentation is not valid website")]
        public string Presentation { get; set; }

        [DisplayName("Contract Template")]
        public string ContractTemplate { get; set; }

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }
<<<<<<< HEAD

        [ScaffoldColumn(false)]
        public int? ClientContractId { get; set; }
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
    }
}