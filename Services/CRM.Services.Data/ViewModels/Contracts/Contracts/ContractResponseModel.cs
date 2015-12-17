using System.Collections.Generic;
using CRM.Data.Models;
using CRM.Services.Data.ViewModels.Contracts.Clients;
using CRM.Services.Data.ViewModels.Contracts.Invoices;
using CRM.Services.Data.ViewModels.Contracts.Providers;
using CRM.Services.Data.ViewModels.Contracts.Trds;
using CRM.Services.Data.ViewModels.Contracts.TypeOfCompanies;

namespace CRM.Services.Data.ViewModels.Contracts.Contracts
{
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
