namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    using System.Linq;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class ClientReportModel : IMapFrom<Client>, IHaveCustomMappings
    {
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
        [DisplayName("Dealr name")]
        public string Dealer { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [DisplayName("Want to receive EPG")]
        public bool WantToReceiveEpg { get; set; }

        [DisplayName("Want to receive Highlights / News")]
        public bool WantToReceiveNews { get; set; }

        public bool HasUnpaidInvoices { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Client, ClientReportModel>()
                .ForMember(c => c.Dealer, opts => opts.MapFrom(c => c.Dealer.UserName))
                .ForMember(c => c.HasUnpaidInvoices, opts => opts.MapFrom(c => c
                    .Contracts.ToList()
                        .Any(contract => contract
                            .Invoices
                            .Any(invoice => invoice.IsPaid == false))));
        }
    }
}