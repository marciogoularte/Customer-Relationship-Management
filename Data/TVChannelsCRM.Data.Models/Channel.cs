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

        public string Website { get; set; }

        public string Presentation { get; set; }

        public string ContractTemplate { get; set; }

<<<<<<< HEAD
        public string LogoLink { get; set; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

<<<<<<< HEAD
        public int? ClientContractId { get; set; }

        public virtual ClientContract ClientContract { get; set; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public string Comments { get; set; } 
    }
}
