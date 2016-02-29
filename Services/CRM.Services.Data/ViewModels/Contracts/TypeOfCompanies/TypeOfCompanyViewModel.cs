namespace CRM.Services.Data.ViewModels.Contracts.TypeOfCompanies
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class TypeOfCompanyViewModel : IMapFrom<TypeOfCompany>
    {
        //public static Expression<Func<TypeOfCompany, TypeOfCompanyViewModel>> FromTypeOfCompany
        //{
        //    get
        //    {
        //        return t => new TypeOfCompanyViewModel()
        //        {
        //            Id = t.Id,
        //            Type = t.Type,
        //            TypeInBulgarian = t.TypeInBulgarian
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [DisplayName("Type in Bulgarian")]
        public string TypeInBulgarian { get; set; }
    }
}