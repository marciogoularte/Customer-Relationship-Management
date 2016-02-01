namespace CRM.Data.Models.Finance
{
    using System;

    using Contracts;

    public class Frz : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public string NumberOfContract { get; set; }

        public DateTime DateOfContract { get; set; }

        public long Salary { get; set; }

        public string BankAccount { get; set; }
        
        public bool IsVisible { get; set; }
    }
}
