namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Providers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

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
<<<<<<< HEAD
                    TypeId = p.TypeId,
                    Uic = p.Uic,
                    Vat = p.Vat,
                    BankAccount = p.BankAccount,
                    ResidenceAndAddress = p.ResidenceAndAddress,
                    ContractTemplate = p.ContractTemplate,
=======
                    Type = p.Type,
                    Eik = p.Eik,
                    BankAccount = p.BankAccount,
                    ResidenceAndAddress = p.ResidenceAndAddress,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
                    NetworkPage = p.NetworkPage,
                    ContactPerson = p.ContactPerson,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address,
                    Term = p.Term,
                    Cps = p.Cps,
<<<<<<< HEAD
                    LogoLink = p.LogoLink,
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
        public string TypeId { get; set; }

        [DisplayName("UIC")]
        public string Uic { get; set; }

        [DisplayName("VAT#")]
        public string Vat { get; set; }
=======
        public TypeOfCompany Type { get; set; }

        [DisplayName("EIK")]
        public string Eik { get; set; }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

        [DisplayName("Bank Account")]
        public string BankAccount { get; set; }

        [DisplayName("Residence and address")]
        public string ResidenceAndAddress { get; set; }

        [DisplayName("Network page")]
        public string NetworkPage { get; set; }

        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Provider email is not valid!")]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Term { get; set; }

        [DisplayName("Cps(years)")]
        public int? Cps { get; set; }

<<<<<<< HEAD
        [DisplayName("Logo Link")]
        [Url(ErrorMessage = "Channel Epg Source is not valid website")]
        public string LogoLink { get; set; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        public string Commission { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }
<<<<<<< HEAD

        [DisplayName("Contract Template")]
        [UIHint("ContractTemplateEditor")]
        public ContractTemplate ContractTemplate { get; set; }
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
    }
}