namespace CRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Provider : DeletableEntity, IEntity
    {
        private ICollection<Channel> channels;
        private ICollection<ProviderContract> contracts;
        private ICollection<Discussion> discussions;

        public Provider()
        {
            this.channels = new HashSet<Channel>();
            this.contracts = new HashSet<ProviderContract>();
            this.discussions = new HashSet<Discussion>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 40 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string TypeId { get; set; }

        public string Uic { get; set; }

        public string Vat { get; set; }

        public string BankAccount { get; set; }

        public string ResidenceAndAddress { get; set; }

        public string NetworkPage { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Term { get; set; }

        public int? Cps { get; set; }

        public string Commission { get; set; }

        public string LogoLink { get; set; }

        public string Comments { get; set; }

        public ContractTemplate ContractTemplate { get; set; }

        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        }

        public virtual ICollection<ProviderContract> Contracts
        {
            get { return this.contracts; }
            set { this.contracts = value; }
        }

        public virtual ICollection<Discussion> Discussions
        {
            get { return this.discussions; }
            set { this.discussions = value; }
        }
    }
}
