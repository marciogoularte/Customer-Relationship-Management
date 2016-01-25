using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;

namespace CRM.Services.Data.ViewModels.Contracts.Clients
{
    using Web.Common.Mappings;

    public class ClientViewModel : IMapFrom<Client>
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
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string Name { get; set; }

        [DisplayName("Bulgarian Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 20 symbols")]
        public string NameBulgarian { get; set; }

        [Required]
        [DisplayName("Type")]
        [UIHint("TypeOfCompanyEditor")]
        public string TypeOfCompany { get; set; }

        [DisplayName("UIC")]
        [Required(ErrorMessage = "UIC is required")]
        public string Uic { get; set; }

        [Required]
        [DisplayName("VAT#")]
        public string Vat { get; set; }

        [Required]
        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

        [Required]
        [DisplayName("Residence and address(BG)")]
        public string ResidenceAndAddressInBulgarian { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        [DisplayName("Network page")]
        public string NetworkPage { get; set; }

        [Required]
        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [Required]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Correspondence")]
        public string Correspondence { get; set; }

        [DisplayName("Fixed phone service")]
        public double FixedPhoneService { get; set; }

        [DisplayName("Mobile voice services provided through network")]
        public double MobileVoiceServicesProvidedThroughNetwork { get; set; }

        [DisplayName("Internet subs")]
        public double InternetSubs { get; set; }

        [DisplayName("Services mobile access to internet")]
        public double ServicesMobileAccessToInternet { get; set; }

        [DisplayName("TV Subs")]
        public double TvSubs { get; set; }

        public string Coverage { get; set; }

        [DisplayName("Post code")]
        public string PostCode { get; set; }

        [Required]
        public string Management { get; set; }

        [Required]
        [DisplayName("Managment in Bulgarian")]
        public string ManagementInBulgarian { get; set; }

        [Required]
        [DisplayName("Management phone")]
        public string ManagementPhone { get; set; }

        [DisplayName("Management email")]
        public string ManagementEmail { get; set; }

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

        [Required]
        [DisplayName("Dealer name")]
        public string DealerName { get; set; }

        [Required]
        [DisplayName("Dealer phone")]
        public string DealerPhone { get; set; }

        [Required]
        [DisplayName("Dealer email")]
        public string DealerEmail { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [Required]
        [DisplayName("Want to receive EPG")]
        public bool WantToReceiveEpg { get; set; }

        [Required]
        [DisplayName("Want to receive Highlights / News")]
        public bool WantToReceiveNews { get; set; }
    }
}