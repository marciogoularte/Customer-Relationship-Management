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

        [UIHint("TextAreaEditor")]
        public string SatelliteData { get; set; }

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
