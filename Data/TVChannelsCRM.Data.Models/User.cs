using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Contracts;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IEntity
    {
        private ICollection<Activity> lastActivities;
        private ICollection<SchedulerTask> schedulerTasks;

        public User()
        {
            this.lastActivities = new HashSet<Activity>();
            this.schedulerTasks = new HashSet<SchedulerTask>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "First name length should be between 2 and 40 chars")]
        public string FirstName { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Second name length should be between 2 and 40 chars")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Last name length should be between 2 and 40 chars")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Range(10, 100, ErrorMessage = "Age should be in range 10 - 100")]
        public int? Age { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Town length should be between 2 and 40 chars")]
        public string Town { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Country length should be between 2 and 40 chars")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enterprise position is required")]
        public EnterprisePosition EnterprisePosition { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedOn { get; set; }

        public string Website { get; set; }

        public virtual ICollection<Activity> LastActivities
        {
            get { return this.lastActivities; }
            set { this.lastActivities = value; }
        }

        public virtual ICollection<SchedulerTask> SchedulerTasks
        {
            get { return this.schedulerTasks; }
            set { this.schedulerTasks = value; }
        }
    }
}
