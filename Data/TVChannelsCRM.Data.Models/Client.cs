namespace TVChannelsCRM.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class Client : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActiveFrom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActiveTo { get; set; }

        //public int MyProperty { get; set; }

        public string Mg { get; set; }

        public string IrdCard { get; set; }

        // TODO: work on it, probably invoicing with enumeration
        public string Invoicing { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfSigning { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfExpiring { get; set; }

        public string Currency { get; set; }

        public string InvoicesIssued { get; set; }

        public string PaymentsReceived { get; set; }

        public string Contract { get; set; }
        // TODO: uncomment lists
        //public List<string> SpecialConditions { get; set; }
    }
}
