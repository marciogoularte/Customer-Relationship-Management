namespace CRM.Services.Data.ViewModels.Marketing.Reports
{
    using System.ComponentModel;

    using CRM.Data.Models;
    using Web.Common.Mappings;

    public class EpgReportModel : IMapFrom<Client>
    {
        [DisplayName("Operator name")]
        public string Name { get; set; }

        [DisplayName("Bulgarian name")]
        public string NameBulgarian { get; set; }

        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        public string Email { get; set; }

        [DisplayName("Marketing name")]
        public string Marketing { get; set; }

        [DisplayName("Marketing phone")]
        public string MarketingPhone { get; set; }

        [DisplayName("Marketing email")]
        public string MarketingEmail { get; set; }

        [DisplayName("Want to receive epg")]
        public bool WantToReceiveEpg { get; set; }
    }
}