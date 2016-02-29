namespace CRM.Services.Data.ViewModels.Marketing.Partners
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Web.Common.Mappings;
    using CRM.Data.Models.Marketing;

    public class MediaViewModel : IMapFrom<Media>
    {
        //public static Expression<Func<Media, MediaViewModel>> FromMedia
        //{
        //    get
        //    {
        //        return p => new MediaViewModel()
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Address = p.Address,
        //            PhoneNumber = p.PhoneNumber,
        //            Email = p.Email,
        //            AllMedia = p.AllMedia
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsVisible { get; set; }

        [DisplayName("Media")]
        public string AllMedia { get; set; }
    }
}
