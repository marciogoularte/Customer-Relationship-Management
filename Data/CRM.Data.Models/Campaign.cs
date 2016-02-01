namespace CRM.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    public class Campaign : DeletableEntity, IEntity
    {
        private ICollection<Provider> providers;
        private ICollection<Client> clients;

        public Campaign()
        {
            this.providers = new HashSet<Provider>();
            this.clients = new HashSet<Client>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Activities { get; set; }

        public double Budget { get; set; }

        public string Results { get; set; }

        public bool IsVisible { get; set; }

        public virtual ICollection<Provider> Providers
        {
            get { return this.providers; }
            set { this.providers = value; }
        }

        public virtual ICollection<Client> Clients
        {
            get { return this.clients; }
            set { this.clients = value; }
        }
    }
}
