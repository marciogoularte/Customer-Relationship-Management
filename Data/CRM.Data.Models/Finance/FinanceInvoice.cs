namespace CRM.Data.Models.Finance
{
    using System;

    using Contracts;

    public class FinanceInvoice : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string NumberOfInvoice { get; set; }

        public DateTime Date { get; set; }

        public string Receiver { get; set; }

        public string Preview { get; set; }

        public long SumWithoutDdS { get; set; }

        public long SumWithDds { get; set; }
    }
}