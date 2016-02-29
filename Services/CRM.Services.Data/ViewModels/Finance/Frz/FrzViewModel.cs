namespace CRM.Services.Data.ViewModels.Finance.Frz
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Web.Common.Mappings;
    using CRM.Data.Models.Finance;

    public class FrzViewModel : IMapFrom<Frz>
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }

        [DisplayName("Number of contract")]
        public string NumberOfContract { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of contract")]
        public DateTime DateOfContract { get; set; }

        public long Salary { get; set; }

        [DisplayName("Bank account")]
        public string BankAccount { get; set; }

        [DisplayName("Is visible")]
        public bool IsVisible { get; set; }
    }
}
