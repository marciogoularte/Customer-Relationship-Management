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
                    Comments = c.Comments,
                    Provider = c.Provider,
                    Client = c.Client
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
        public string EpgSource { get; set; }

        [DisplayName("Website")]
        [Url(ErrorMessage = "Provider website is not valid")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Presentation is not valid website")]
        public string Presentation { get; set; }

        [DisplayName("Contract Template")]
        public string ContractTemplate { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public virtual Provider Provider { get; set; }

        [ScaffoldColumn(false)]
        public virtual Client Client { get; set; }
    }
}