namespace CRM.Services.Data.ViewModels.Users.Contracts
{
    using System.Collections.Generic;

    using Trds;
    using Clients;
    using Invoices;
    using Providers;
    using TypeOfCompanies;
    using CRM.Data.Models;

    public class ContractResponseModel
    {
        public double Sum { get; set; }

        public ClientViewModel Client { get; set; }

        public ProviderViewModel Provider { get; set; }

        public List<ChannelViewModel> Channels { get; set; }

        public List<InvoiceViewModel> Invoices { get; set; }

        public List<TrdViewModel> Trds { get; set; }

        public double MgSubs { get; set; }

        public double Cps { get; set; }

        public TypeOfCompanyViewModel ClientContractType { get; set; }

        public ClientContractViewModel Contract { get; set; }

        public ContractTemplate ContractTemplate { get; set; }
    }
}
