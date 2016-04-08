namespace CRM.Services.Data.ViewModels.Contracts.Clients
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class ClientViewModel : IMapFrom<Client>, IHaveCustomMappings
    {
        //public static Expression<Func<Client, ClientViewModel>> FromClient
        //{
        //    get
        //    {
        //        return c => new ClientViewModel()
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            NameBulgarian = c.NameBulgarian,
        //            TypeOfCompany = c.Type,
        //            Uic = c.Uic,
        //            Vat = c.Vat,
        //            ResidenceAndAddress = c.ResidenceAndAddress,
        //            ResidenceAndAddressInBulgarian = c.ResidenceAndAddressInBulgarian,
        //            NetworkPage = c.NetworkPage,
        //            ContactPerson = c.ContactPerson,
        //            PhoneNumber = c.PhoneNumber,
        //            Email = c.Email,
        //            Correspondence = c.Correspondence,
        //            FixedPhoneService = c.FixedPhoneService,
        //            MobileVoiceServicesProvidedThroughNetwork = c.MobileVoiceServicesProvidedThroughNetwork,
        //            InternetSubs = c.InternetSubs,
        //            ServicesMobileAccessToInternet = c.ServicesMobileAccessToInternet,
        //            TvSubs = c.TvSubs,
        //            Coverage = c.Coverage,
        //            PostCode = c.PostCode,
        //            Management = c.Management,
        //            ManagementInBulgarian = c.ManagementInBulgarian,
        //            ManagementPhone = c.ManagementPhone,
        //            ManagementEmail = c.ManagementEmail,
        //            Finance = c.Finance,
        //            FinancePhone = c.FinancePhone,
        //            FinanceEmail = c.FinanceEmail,
        //            TechnicalName = c.TechnicalName,
        //            TechnicalPhone = c.TechnicalPhone,
        //            TechnicalEmail = c.TechnicalEmail,
        //            Marketing = c.Marketing,
        //            MarketingPhone = c.MarketingPhone,
        //            MarketingEmail = c.MarketingEmail,
        //            DealerName = c.DealerName,
        //            DealerEmail = c.DealerEmail,
        //            DealerPhone = c.DealerPhone,
        //            WantToReceiveEpg = c.WantToReceiveEpg,
        //            WantToReceiveNews = c.WantToReceiveNews,
        //            Comments = c.Comments
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string Name { get; set; }

        [DisplayName("Bulgarian Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string NameBulgarian { get; set; }

        [DisplayName("Type")]
        [UIHint("TypeOfCompanyEditor")]
        public string TypeOfCompany { get; set; }

        [DisplayName("UIC")]
        public string Uic { get; set; }

        [DisplayName("VAT#")]
        public string Vat { get; set; }

        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

        [DisplayName("Residence and address(BG)")]
        public string ResidenceAndAddressInBulgarian { get; set; }
        
        public string Region { get; set; }
        
        [DisplayName("Network page")]
        public string NetworkPage { get; set; }
        
        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }
        
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        
        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }
        
        [DisplayName("Correspondence")]
        public string Correspondence { get; set; }

        [DisplayName("Fixed phone service")]
        public double? FixedPhoneService { get; set; }

        [DisplayName("Mobile voice services provided through network")]
        public double? MobileVoiceServicesProvidedThroughNetwork { get; set; }

        [DisplayName("Internet subs")]
        public double? InternetSubs { get; set; }

        [DisplayName("Services mobile access to internet")]
        public double? ServicesMobileAccessToInternet { get; set; }

        [DisplayName("TV Subs")]
        public double? TvSubs { get; set; }

        public string Coverage { get; set; }

        [DisplayName("Post code")]
        public string PostCode { get; set; }
        
        public string Management { get; set; }
        
        [DisplayName("Managment in Bulgarian")]
        public string ManagementInBulgarian { get; set; }
        
        [DisplayName("Management phone")]
        public string ManagementPhone { get; set; }

        [DisplayName("Management email")]
        public string ManagementEmail { get; set; }

        public string Finance { get; set; }

        [DisplayName("Finance phone")]
        public string FinancePhone { get; set; }

        [DisplayName("Finance address")]
        public string FinanceAddress { get; set; }

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
        
        [Required]
        [UIHint("DealerEditor")]
        public string Dealer { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }
        
        [DisplayName("Want to receive EPG")]
        public bool WantToReceiveEpg { get; set; }
        
        [DisplayName("Want to receive Highlights / News")]
        public bool WantToReceiveNews { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Client, ClientViewModel>()
                .ForMember(m => m.Dealer, opts => opts.MapFrom(m => m.Dealer.UserName.ToString()));
        }
    }
}