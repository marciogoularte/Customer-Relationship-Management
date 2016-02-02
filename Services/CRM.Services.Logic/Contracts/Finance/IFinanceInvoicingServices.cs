namespace CRM.Services.Logic.Contracts.Finance
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.FinanceInvoice;

    public interface IFinanceInvoicingServices : IService
    {
        List<string> Index();

        FinanceInvoiceViewModel FinanceInvoiceInformation(int invoiceId);

        FinanceInvoiceViewModel FinanceInvoiceDetails(int invoiceId);

        List<FinanceInvoiceViewModel> ReadFinanceInvoices(string searchboxinvoice, bool showAll);

        FinanceInvoiceViewModel CreateFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice);

        FinanceInvoiceViewModel UpdateFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice);

        FinanceInvoiceViewModel DestroyFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice);

        string GetFinanceInvoicById(int invoiceId);
    }
}
