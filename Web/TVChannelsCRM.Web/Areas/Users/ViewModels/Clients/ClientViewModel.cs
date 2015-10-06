namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Clients
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class ClientViewModel
    {
        public static Expression<Func<Client, ClientViewModel>> FromClient
        {
            get
            {
                return c => new ClientViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
<<<<<<< HEAD
                    NameBulgarian = c.NameBulgarian,
                    TypeId = c.Type,
                    Uic = c.Uic,
                    Vat = c.Vat,
=======
                    Type = c.Type,
                    Eik = c.Eik,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                    ResidenceAndAddress = c.ResidenceAndAddress,
                    NetworkPage = c.NetworkPage,
                    ContactPerson = c.ContactPerson,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
<<<<<<< HEAD
                    Correspondence = c.Correspondence,
                    FixedPhoneService = c.FixedPhoneService,
                    MobileVoiceServicesProvidedThroughNetwork = c.MobileVoiceServicesProvidedThroughNetwork,
                    InternetSubs = c.InternetSubs,
                    ServicesMobileAccessToInternet = c.ServicesMobileAccessToInternet,
                    TvSubs = c.TvSubs,
                    Coverage = c.Coverage,
=======
                    SecondaryAddress = c.SecondaryAddress,
                    ActiveCable = c.ActiveCable,
                    FixedPhoneService = c.FixedPhoneService,
                    AccessToPublicServiceThroughChoiceOperator = c.AccessToPublicServiceThroughChoiceOperator,
                    MobileVoiceServicesProvidedThroughNetwork = c.MobileVoiceServicesProvidedThroughNetwork,
                    PublicServicesProvidedByWirelessAccess = c.PublicServicesProvidedByWirelessAccess,
                    ServicesFixedAccessToInternet = c.ServicesFixedAccessToInternet,
                    ServicesMobileAccessToInternet = c.ServicesMobileAccessToInternet,
                    ServicesTransmissionData = c.ServicesTransmissionData,
                    SpreadingRadioAndTvPrograms = c.SpreadingRadioAndTvPrograms,
                    Coverage = c.Coverage,
                    CorrespondenceAddress = c.CorrespondenceAddress,
                    CorAddress = c.CorAddress,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                    PostCode = c.PostCode,
                    Management = c.Management,
                    ManagementPhone = c.ManagementPhone,
                    ManagementEmail = c.ManagementEmail,
<<<<<<< HEAD
=======
                    ManagementTeritory = c.ManagementTeritory,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                    Finance = c.Finance,
                    FinancePhone = c.FinancePhone,
                    FinanceEmail = c.FinanceEmail,
                    TechnicalName = c.TechnicalName,
                    TechnicalPhone = c.TechnicalPhone,
                    TechnicalEmail = c.TechnicalEmail,
                    Marketing = c.Marketing,
                    MarketingPhone = c.MarketingPhone,
<<<<<<< HEAD
                    MarketingEmail = c.MarketingEmail,
                    Frequency = c.Frequency
=======
                    MarketingEmail = c.MarketingEmail
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
<<<<<<< HEAD
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string Name { get; set; }

        [DisplayName("Bulgarian Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string NameBulgarian { get; set; }

        [UIHint("TypeOfCompanyEditor")]
        public string TypeId { get; set; }

        [DisplayName("UIC")]
        [Required(ErrorMessage = "UIC is required")]
        public string Uic { get; set; }

        [DisplayName("VAT#")]
        public string Vat { get; set; }
=======
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string Name { get; set; }

        [UIHint("TypeOfCompanyEditor")]
        public TypeOfCompany Type { get; set; }

        [Required(ErrorMessage = "EIK is required")]
        public string Eik { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

<<<<<<< HEAD
        [UIHint("FrequencyEditor")]
        public Frequency Frequency { get; set; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        [DisplayName("Network page")]
        public string NetworkPage { get; set; }

        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

<<<<<<< HEAD
        [DisplayName("Correspondence")]
        public string Correspondence { get; set; }
=======
        [DisplayName("Secondary address")]
        public string SecondaryAddress { get; set; }

        [DisplayName("Active cable")]
        public double ActiveCable { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Fixed phone service")]
        public double FixedPhoneService { get; set; }

<<<<<<< HEAD
        [DisplayName("Mobile voice services provided through network")]
        public double MobileVoiceServicesProvidedThroughNetwork { get; set; }

        [DisplayName("Internet subs")]
        public double InternetSubs { get; set; }
=======
        [DisplayName("Public service through choice operator")]
        public double AccessToPublicServiceThroughChoiceOperator { get; set; }

        [DisplayName("Mobile voice services provided through network")]
        public double MobileVoiceServicesProvidedThroughNetwork { get; set; }

        [DisplayName("Public services provided by wireless access")]
        public double PublicServicesProvidedByWirelessAccess { get; set; }

        [DisplayName("Services fixed access to internet")]
        public double ServicesFixedAccessToInternet { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Services mobile access to internet")]
        public double ServicesMobileAccessToInternet { get; set; }

<<<<<<< HEAD
        [DisplayName("TV Subs")]
        public double TvSubs { get; set; }

        public string Coverage { get; set; }

=======
        [DisplayName("Services transmission data")]
        public double ServicesTransmissionData { get; set; }

        [DisplayName("Spreading radio and TV programs")]
        public double SpreadingRadioAndTvPrograms { get; set; }

        public string Coverage { get; set; }

        [DisplayName("Correspondence address")]
        public string CorrespondenceAddress { get; set; }

        [DisplayName("Cor address")]
        public string CorAddress { get; set; }

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        [DisplayName("Post code")]
        public string PostCode { get; set; }

        public string Management { get; set; }

        [DisplayName("Management phone")]
        public string ManagementPhone { get; set; }

        [DisplayName("Management email")]
        public string ManagementEmail { get; set; }

<<<<<<< HEAD
=======
        [DisplayName("Management teritory")]
        public string ManagementTeritory { get; set; }

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public string Finance { get; set; }

        [DisplayName("Finance phone")]
        public string FinancePhone { get; set; }

        [DisplayName("Finance email")]
        public string FinanceEmail { get; set; }

        [DisplayName("Technical name")]
        public string TechnicalName { get; set; }

        [DisplayName("Technical phone")]
        public string TechnicalPhone { get; set; }

        [DisplayName("Technical email")]
        public string TechnicalEmail { get; set; }

        public string Marketing { get; set; }

        [DisplayName("Marketing phone")]
        public string MarketingPhone { get; set; }

        [DisplayName("Marketing email")]
        public string MarketingEmail { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }
    }
}