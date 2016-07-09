namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Invoices;

    public interface IInvoicesServices : IService
    {
        List<string> GetContractInvoicesData();

<<<<<<< HEAD
        void InvoiceIsPaid(int invoiceId);

=======
>>>>>>> d5b65130ac06472e570e2926b4106b53b6bd5ff6
        List<InvoiceViewModel> ReadContractInvoices(string searchbox, int contractId, bool showAll);

        InvoiceViewModel CreateContractInvoice(InvoiceViewModel invoice, int contractId);

        InvoiceViewModel InvoiceDetails(int invoiceId);

        InvoiceViewModel UpdateContractInvoice(InvoiceViewModel invoice);

        InvoiceViewModel DestroyClientInvoice(InvoiceViewModel invoice);
    }
}