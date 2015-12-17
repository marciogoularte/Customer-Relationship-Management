namespace CRM.Services.Data.ViewModels.Finance.Frz
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using CRM.Data.Models.Finance;

    public class FrzViewModel
    {
        public static Expression<Func<Frz, FrzViewModel>> FromFrz
        {
            get
            {
                return fi => new FrzViewModel()
                {
                    Id = fi.Id,
                    EmployeeName = fi.EmployeeName,
                    NumberOfContract = fi.NumberOfContract,
                    DateOfContract = fi.DateOfContract,
                    Salary = fi.Salary,
                    BankAccount = fi.BankAccount
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public string NumberOfContract { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfContract { get; set; }

        public long Salary { get; set; }

        public string BankAccount { get; set; }
    }
}
