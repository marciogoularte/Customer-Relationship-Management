namespace TVChannelsCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Provider : DeletableEntity, IEntity
    {
        private ICollection<Channel> channels;
        private ICollection<Contract> contracts;

        public Provider()
        {
            this.channels = new HashSet<Channel>();
            this.contracts = new HashSet<Contract>();
        }

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

        public string Term { get; set; }

        public string CPS { get; set; }

        public string Commission { get; set; }

        public string Comments { get; set; }

        public ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        }

        public ICollection<Contract> Contracts
        {
            get { return this.contracts; }
            set { this.contracts = value; }
        }
    }
}
