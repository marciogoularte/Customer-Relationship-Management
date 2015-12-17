using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;

namespace CRM.Services.Data.ViewModels.Contracts.Providers
{
    public class ProviderViewModel
    {
        public static Expression<Func<Provider, ProviderViewModel>> FromProvider
        {
            get
            {
                return p => new ProviderViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    TypeId = p.TypeId,
                    Uic = p.Uic,
                    Vat = p.Vat,
                    BankAccount = p.BankAccount,
                    ResidenceAndAddress = p.ResidenceAndAddress,
                    ContractTemplate = p.ContractTemplate,
                    NetworkPage = p.NetworkPage,
                    ContactPerson = p.ContactPerson,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address,
                    Term = p.Term,
                    Cps = p.Cps,
                    LogoLink = p.LogoLink,
                    Commission = p.Commission
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 40 chars")]
        public string Name { get; set; }
        
        [UIHint("TypeOfCompanyEditor")]
        [Required(ErrorMessage = "Type is required")]
        public string TypeId { get; set; }

        [Required]
        [DisplayName("UIC")]
        public string Uic { get; set; }

        [Required]
        [DisplayName("VAT#")]
        public string Vat { get; set; }

        [Required]
        [DisplayName("Bank Account")]
        public string BankAccount { get; set; }

        [Required]
        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

        [Required]
        [DisplayName("Network page")]
        public string NetworkPage { get; set; }

        [Required]
        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Term { get; set; }

        [Required]
        [DisplayName("Cps(years)")]
        public int? Cps { get; set; }

        [DisplayName("Logo Link")]
        [Url(ErrorMessage = "Channel Epg Source is not valid website")]
        public string LogoLink { get; set; }

        public string Commission { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [DisplayName("Contract Template")]
        [UIHint("ContractTemplateEditor")]
        public ContractTemplate ContractTemplate { get; set; }
    }
}