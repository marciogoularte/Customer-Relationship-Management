namespace CRM.Services.Logic.Contracts.Users
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
   
    using Base;
    using Data.ViewModels.Users.Invoices;

    public interface IInvoicesServices : IService
    {
        List<string> GetContractInvoicesData();

        List<InvoiceViewModel> ReadContractInvoices(string searchbox, int contractId);

        InvoiceViewModel CreateContractInvoice(InvoiceViewModel invoice, int contractId);

        Task<InvoiceViewModel> InvoiceDetails(int invoiceId);

        InvoiceViewModel UpdateContractInvoice(InvoiceViewModel invoice);

        InvoiceViewModel DestroyClientInvoice(InvoiceViewModel invoice);
    }
}