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
                    Type = p.Type,
                    Eik = p.Eik,
                    ResidenceAndAddress = p.ResidenceAndAddress,
                    NetworkPage = p.NetworkPage,
                    ContactPerson = p.ContactPerson,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address,
                    Term = p.Term,
                    CPS = p.CPS,
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
        public TypeOfCompany Type { get; set; }

        [DisplayName("EIK")]
        public string Eik { get; set; }

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

        public string CPS { get; set; }

        public string Commission { get; set; }

        [UIHint("CommentsEditor")]
        public string Comments { get; set; }
    }
}