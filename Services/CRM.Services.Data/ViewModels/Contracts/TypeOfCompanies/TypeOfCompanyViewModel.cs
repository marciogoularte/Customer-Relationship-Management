using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;
using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Contracts.TypeOfCompanies
{
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