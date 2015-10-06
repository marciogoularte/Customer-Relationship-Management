namespace TVChannelsCRM.Data.Models
{
    using System;

    using Contracts;

    public class Invoice : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string MgSubs { get; set; }

        public string Cps { get; set; }

        public string Comments { get; set; }

        public int? ClientContractId { get; set; }
        
        public virtual ClientContract ClientContract { get; set; }
    }
}