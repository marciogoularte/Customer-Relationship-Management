namespace TVChannelsCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Client : DeletableEntity, IEntity
    {
        private ICollection<ClientContract> contracts;
        private ICollection<Discussion> discussions;
<<<<<<< HEAD
        private ICollection<Trd> trds;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        public Client()
        {
            this.contracts = new HashSet<ClientContract>();
            this.discussions = new HashSet<Discussion>();
<<<<<<< HEAD
            this.trds = new HashSet<Trd>();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        }

        public int Id { get; set; }

        public string Name { get; set; }

<<<<<<< HEAD
        public string NameBulgarian { get; set; }

        public string Type { get; set; }

        public Frequency Frequency { get; set; }

        public string Uic { get; set; }

        public string Vat { get; set; }
=======
        public TypeOfCompany Type { get; set; }

        public string Eik { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        public string ResidenceAndAddress { get; set; }

        public string NetworkPage { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

<<<<<<< HEAD
        public string Correspondence { get; set; }
               
        public double FixedPhoneService { get; set; }
           
        public double MobileVoiceServicesProvidedThroughNetwork { get; set; }

        public double InternetSubs { get; set; }
               
        public double ServicesMobileAccessToInternet { get; set; }
               
        public double TvSubs { get; set; }

        public string Coverage { get; set; }

=======
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

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public string PostCode { get; set; }

        public string Management { get; set; }

        public string ManagementPhone { get; set; }

        public string ManagementEmail { get; set; }

<<<<<<< HEAD
=======
        public string ManagementTeritory { get; set; }

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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

<<<<<<< HEAD
        public virtual ICollection<Trd> Trds
        {
            get { return this.trds; }
            set { this.trds = value; }
        }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
    }
}
