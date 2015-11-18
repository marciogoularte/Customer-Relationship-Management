namespace TVChannelsCRM.Web.Areas.Users.ViewModels.TypeOfCompanies
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class TypeOfCompanyViewModel
    {
        public static Expression<Func<TypeOfCompany, TypeOfCompanyViewModel>> FromTypeOfCompany
        {
            get
            {
                return t => new TypeOfCompanyViewModel()
                {
                    Id = t.Id,
                    Type = t.Type,
                    TypeInBulgarian = t.TypeInBulgarian
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [DisplayName("Type in Bulgarian")]
        public string TypeInBulgarian { get; set; }
    }
}