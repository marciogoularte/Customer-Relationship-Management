namespace CRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Client : DeletableEntity, IEntity
    {
        private ICollection<ClientContract> contracts;
        private ICollection<Discussion> discussions;
        private ICollection<Trd> trds;
        private ICollection<Campaign> campaigns;

        public Client()
        {
            this.contracts = new HashSet<ClientContract>();
            this.discussions = new HashSet<Discussion>();
            this.trds = new HashSet<Trd>();
            this.campaigns = new HashSet<Campaign>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameBulgarian { get; set; }
        
        public string TypeOfCompany { get; set; }

        public string Uic { get; set; }

        public string Vat { get; set; }

        public string ResidenceAndAddress { get; set; }

        public string ResidenceAndAddressInBulgarian { get; set; }

        public string Region { get; set; }

        public string NetworkPage { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string Correspondence { get; set; }
               
        public double? FixedPhoneService { get; set; }
           
        public double? MobileVoiceServicesProvidedThroughNetwork { get; set; }

        public double? InternetSubs { get; set; }
               
        public double? ServicesMobileAccessToInternet { get; set; }
               
        public double? TvSubs { get; set; }

        public string Coverage { get; set; }

        public string PostCode { get; set; }

        public string Management { get; set; }

        public string ManagementInBulgarian { get; set; }

        public string ManagementPhone { get; set; }

        public string ManagementEmail { get; set; }

        public string Finance { get; set; }

        public string FinancePhone { get; set; }

        public string FinanceAddress { get; set; }

        public string FinanceEmail { get; set; }

        public string TechnicalName { get; set; }

        public string TechnicalPhone { get; set; }

        public string TechnicalEmail { get; set; }

        public string Marketing { get; set; }

        public string MarketingPhone { get; set; }

        public string MarketingEmail { get; set; }

        public string Comments { get; set; }
        
        public string DealerName { get; set; }
        
        public string DealerPhone { get; set; }
        
        public string DealerEmail { get; set; }
        
        public bool WantToReceiveEpg { get; set; }

        public bool WantToReceiveNews { get; set; }
        
        public bool IsVisible { get; set; }

        public virtual ICollection<Trd> Trds
        {
            get { return this.trds; }
            set { this.trds = value; }
        }

        public virtual ICollection<ClientContract> Contracts
        {
            get { return this.contracts; }
            set { this.contracts = value; }
        }

        public virtual ICollection<Discussion> Discussions
        {
            get { return this.discussions; }
            set { this.discussions = value; }
        }

        public virtual ICollection<Campaign> Campaigns
        {
            get { return this.campaigns; }
            set { this.campaigns = value; }
        }
    }
}
