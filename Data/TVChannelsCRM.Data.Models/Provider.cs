namespace TVChannelsCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Provider : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 40 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public TypeOfCompany Type { get; set; }

        public string Eik { get; set; }

        public string ResidenceAndAddress { get; set; }

        public string NetworkPage { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string Address { get; set; }

        public string ChannelName { get; set; }

        public string ReveivingOptions { get; set; }

        public string SatelliteData { get; set; }

        public string Degrees { get; set; }

        public string Freq { get; set; }

        public string Transponder { get; set; }

        public string Encryption { get; set; }

        public string SrFec { get; set; }

        public string Sid { get; set; }

        public string Vpid { get; set; }

        public string Apid { get; set; }

        public string OnidTid { get; set; }

        public string Beam { get; set; }

        public string EpgSource { get; set; }

        [Url(ErrorMessage = "Provider website is not valid")]
        public string Website { get; set; }

        public string Presentation { get; set; }

        public string ContractTemplate { get; set; }

        public string Term { get; set; }

        public string CPS { get; set; }

        public string Commission { get; set; }
    }
}
