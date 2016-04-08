namespace CRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Channel : DeletableEntity, IEntity
    {
        private ICollection<ClientContract> clientContracts;

        public Channel()
        {
            this.clientContracts = new HashSet<ClientContract>();
        }

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

        public string LogoLink { get; set; }

        public int? ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public string Comments { get; set; }
        
        public bool IsVisible { get; set; }

        public virtual ICollection<ClientContract> ClientContracts
        {
            get { return this.clientContracts; }
            set { this.clientContracts = value; }
        }
    }
}
