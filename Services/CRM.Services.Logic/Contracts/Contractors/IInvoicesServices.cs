using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Invoices;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface IInvoicesServices : IService
    {
        List<string> GetContractInvoicesData();

        List<InvoiceViewModel> ReadContractInvoices(string searchbox, int contractId);

        InvoiceViewModel CreateContractInvoice(InvoiceViewModel invoice, int contractId);

        InvoiceViewModel InvoiceDetails(int invoiceId);

        InvoiceViewModel UpdateContractInvoice(InvoiceViewModel invoice);

        InvoiceViewModel DestroyClientInvoice(InvoiceViewModel invoice);
    }
}