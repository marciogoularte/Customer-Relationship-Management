using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Marketing.Social
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Marketing;

    public class SocialPartnerViewModel : IMapFrom<SocialPartner>
    {
        //public static Expression<Func<SocialPartner, SocialPartnerViewModel>> FromSocialPartner
        //{
        //    get
        //    {
        //        return s => new SocialPartnerViewModel()
        //        {
        //            Id = s.Id,
        //            Name = s.Name,
        //            Website = s.Website,
        //            Email = s.Email,
        //            PhoneNumber = s.PhoneNumber,
        //            SocialSystem = s.SocialSystem
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Url]
        public string Website { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [ScaffoldColumn(false)]
        public SocialSystemType SocialSystem { get; set; }
    }
}
