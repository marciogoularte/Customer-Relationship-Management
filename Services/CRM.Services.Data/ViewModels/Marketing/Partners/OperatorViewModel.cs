﻿namespace CRM.Services.Data.ViewModels.Marketing.Partners
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Marketing;

    public class OperatorViewModel
    {
        public static Expression<Func<Operator, OperatorViewModel>> FromOperator
        {
            get
            {
                return p => new OperatorViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Media = p.Media
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Media { get; set; }
    }
}
