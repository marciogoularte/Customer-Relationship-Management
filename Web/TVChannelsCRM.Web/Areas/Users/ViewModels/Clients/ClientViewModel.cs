namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Clients
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class ClientViewModel
    {
        public static Expression<Func<Client, ClientViewModel>> FromClient
        {
            get
            {
                return c => new ClientViewModel()
                {
                    Id = c.Id,
                    IsActive = c.IsActive,
                    ActiveFrom = c.ActiveFrom,
                    ActiveTo = c.ActiveTo,
                    Mg = c.Mg,
                    IrdCard = c.IrdCard,
                    Invoicing = c.Invoicing,
                    DateOfSigning = c.DateOfSigning,
                    DateOfExpiring = c.DateOfExpiring,
                    Currency = c.Currency,
                    InvoicesIssued = c.InvoicesIssued,
                    PaymentsReceived = c.PaymentsReceived,
                    Contract = c.Contract
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Is active")]
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Active from")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActiveFrom { get; set; }
        
        [DisplayName("Active to")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActiveTo { get; set; }

        //public int MyProperty { get; set; }

        public string Mg { get; set; }

        [DisplayName("IRD Card")]
        public string IrdCard { get; set; }

        // TODO: work on it, probably invoicing with enumeration
        public string Invoicing { get; set; }

        [DisplayName("Date of signing")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfSigning { get; set; }
        
        [DisplayName("Date of expiring")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfExpiring { get; set; }

        public string Currency { get; set; }

        [DisplayName("Invoices issued")]
        public string InvoicesIssued { get; set; }

        [DisplayName("Payments received")]
        public string PaymentsReceived { get; set; }

        public string Contract { get; set; }
        // TODO: uncomment lists
        //public List<string> SpecialConditions { get; set; }
    }
}