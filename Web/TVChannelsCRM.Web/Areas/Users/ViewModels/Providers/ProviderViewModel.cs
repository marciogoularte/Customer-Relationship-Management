namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Providers
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class ProviderViewModel
    {
        public static Expression<Func<Provider, ProviderViewModel>> FromProvider
        {
            get
            {
                return p => new ProviderViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type,
                    Eik = p.Eik,
                    ResidenceAndAddress = p.ResidenceAndAddress,
                    NetworkPage = p.NetworkPage,
                    ContactPerson = p.ContactPerson,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address,
                    ChannelName = p.ChannelName,
                    ReveivingOptions = p.ReveivingOptions,
                    SatelliteData = p.SatelliteData,
                    Degrees = p.Degrees,
                    Freq = p.Freq,
                    Transponder = p.Transponder,
                    Encryption = p.Encryption,
                    SrFec = p.SrFec,
                    Sid = p.Sid,
                    Vpid = p.Vpid,
                    Apid = p.Apid,
                    OnidTid = p.OnidTid,
                    Beam = p.Beam,
                    EpgSource = p.EpgSource,
                    Website = p.Website,
                    Presentation = p.Presentation,
                    ContractTemplate = p.ContractTemplate,
                    Term = p.Term,
                    CPS = p.CPS,
                    Commission = p.Commission
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 40 chars")]
        public string Name { get; set; }

        [UIHint("TypeOfCompanyEditor")]
        [Required(ErrorMessage = "Type is required")]
        public TypeOfCompany Type { get; set; }

        [DisplayName("EIK")]
        public string Eik { get; set; }

        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

        [DisplayName("Network page")]
        public string NetworkPage { get; set; }

        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string Address { get; set; }

        [DisplayName("Channel name")]
        public string ChannelName { get; set; }

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

        public string ContractTemplate { get; set; }

        public string Term { get; set; }

        public string CPS { get; set; }

        public string Commission { get; set; }
    }
}