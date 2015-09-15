namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Client : DeletableEntity, IEntity
    {
        private ICollection<Channel> channels;
        private ICollection<ClientContract> contracts;

        public Client()
        {
            this.channels = new HashSet<Channel>();
            this.contracts = new HashSet<ClientContract>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public TypeOfCompany Type { get; set; }

        public string Eik { get; set; }

        public string ResidenceAndAddress { get; set; }

        public string NetworkPage { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string SecondaryAddress { get; set; }

        public double ActiveCable { get; set; }
               
        public double FixedPhoneService { get; set; }
               
        public double AccessToPublicServiceThroughChoiceOperator { get; set; }
               
        public double MobileVoiceServicesProvidedThroughNetwork { get; set; }
               
        public double PublicServicesProvidedByWirelessAccess { get; set; }
               
        public double ServicesFixedAccessToInternet { get; set; }
               
        public double ServicesMobileAccessToInternet { get; set; }
               
        public double ServicesTransmissionData { get; set; }
               
        public double SpreadingRadioAndTvPrograms { get; set; }

        public string Coverage { get; set; }

        public string CorrespondenceAddress { get; set; }

        public string CorAddress { get; set; }

        public string PostCode { get; set; }

        public string Management { get; set; }

        public string ManagementPhone { get; set; }

        public string ManagementEmail { get; set; }

        public string ManagementTeritory { get; set; }

        public string Finance { get; set; }

        public string FinancePhone { get; set; }

        public string FinanceEmail { get; set; }

        public string TechnicalName { get; set; }

        public string TechnicalPhone { get; set; }

        public string TechnicalEmail { get; set; }

        public string Marketing { get; set; }

        public string MarketingPhone { get; set; }

        public string MarketingEmail { get; set; }

        public string Comments { get; set; } 

        public ICollection<Channel> Channels
        {
            get { return this.channels; }
            set { this.channels = value; }
        }

        public ICollection<ClientContract> Contracts
        {
            get { return this.contracts; }
            set { this.contracts = value; }
        } 
    }
}
