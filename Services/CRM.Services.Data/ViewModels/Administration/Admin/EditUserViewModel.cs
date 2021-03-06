﻿namespace CRM.Services.Data.ViewModels.Administration.Admin
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    using CRM.Data.Models;
    using Web.Common.Mappings;
    
    public class EditUserViewModel : IMapFrom<User>
    {
        //public static Expression<Func<User, EditUserViewModel>> FromUser
        //{
        //    get
        //    {
        //        return u => new EditUserViewModel()
        //        {
        //            Id = u.Id,
        //            UserName = u.UserName,
        //            PasswordHash = u.PasswordHash,
        //            Email = u.Email,
        //            FirstName = u.FirstName,
        //            SecondName = u.SecondName,
        //            LastName = u.LastName,
        //            Gender = u.Gender,
        //            Age = u.Age,
        //            Town = u.Town,
        //            Country = u.Country,
        //            EnterprisePosition = u.EnterprisePosition,
        //            PhoneNumber = u.PhoneNumber,
        //            Website = u.Website
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "Username length should be between 2 and 35 chars")]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "This is not valid email address")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Email length should be between 2 and 50 chars")]
        public string Email { get; set; }

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

        [UIHint("Integer")]
        [Range(10, 100, ErrorMessage = "Age should be in range 10 - 100")]
        public int? Age { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Town length should be between 2 and 40 chars")]
        public string Town { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "Country length should be between 2 and 40 chars")]
        public string Country { get; set; }

        [UIHint("EnterprisePositionEditor")]
        [DisplayName("Enterprise position")]
        [Required(ErrorMessage = "Enterprise position is required")]
        public EnterprisePosition EnterprisePosition { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Url]
        [UIHint("Url")]
        public string Website { get; set; }

        //[DisplayName("Profile photo")]
        //public byte[] ProfilePhoto { get; set; }
    }
}