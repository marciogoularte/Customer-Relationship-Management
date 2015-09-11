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
                    Degrees = c.Degrees,
                    Freq = c.Freq,
                    Transponder = c.Transponder,
                    Encryption = c.Encryption,
                    SrFec = c.SrFec,
                    Sid = c.Sid,
                    Vpid = c.Vpid,
                    Apid = c.Apid,
                    OnidTid = c.OnidTid,
                    Beam = c.Beam,
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

        [DisplayName("Satellite data")]
        public string SatelliteData { get; set; }

        public string Degrees { get; set; }

        public string Freq { get; set; }

        public string Transponder { get; set; }

        public string Encryption { get; set; }

        [DisplayName("SR-FEC")]
        public string SrFec { get; set; }

        public string Sid { get; set; }

        public string Vpid { get; set; }

        public string Apid { get; set; }

        [DisplayName("ONID-ID")]
        public string OnidTid { get; set; }

        public string Beam { get; set; }

        [DisplayName("EPG-SOURCE")]
        public string EpgSource { get; set; }

        [DisplayName("Website")]
        [Url(ErrorMessage = "Provider website is not valid")]
        public string Website { get; set; }

        public string Presentation { get; set; }

        [DisplayName("Contract Template")]
        public string ContractTemplate { get; set; }

        [UIHint("CommentsEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public virtual Provider Provider { get; set; }

        [ScaffoldColumn(false)]
        public virtual Client Client { get; set; }
    }
}