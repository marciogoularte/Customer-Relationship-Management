namespace CRM.Services.Data.ViewModels.Users.ManageUser
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class ManageUserViewModel : IMapFrom<User>
    {
        [DisplayName("First name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "First name length should be between 2 and 40 chars")]
        public string FirstName { get; set; }

        [DisplayName("Second name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Second name length should be between 2 and 40 chars")]
        public string SecondName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Last name length should be between 2 and 40 chars")]
        public string LastName { get; set; }

        [UIHint("GenderEditor")]
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Range(10, 100, ErrorMessage = "Age should be in range 10 - 100")]
        public int? Age { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Town length should be between 2 and 40 chars")]
        public string Town { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Country length should be between 2 and 40 chars")]
        public string Country { get; set; }

        public string Website { get; set; }

        public bool IsNullUser()
        {
            return string.IsNullOrEmpty(this.FirstName)
                   && string.IsNullOrEmpty(this.SecondName)
                   && string.IsNullOrEmpty(this.LastName)
                   && this.Age == null
                   && string.IsNullOrEmpty(this.Town)
                   && string.IsNullOrEmpty(this.Country)
                   && string.IsNullOrEmpty(this.Website);
        }
    }
}
