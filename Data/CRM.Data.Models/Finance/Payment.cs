namespace CRM.Data.Models.Finance
{
    using System;

    using Contracts;

    public class Payment : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Expense { get; set; }

        public string Payer { get; set; }

        public string Invoice { get; set; }

        public string Amount { get; set; }
    }
}