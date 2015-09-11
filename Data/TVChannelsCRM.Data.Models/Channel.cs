namespace TVChannelsCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Channel : DeletableEntity, IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

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

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public string Comments { get; set; } 
    }
}
