namespace CRM.Services.Logic.Services.Finance
{
    using System.Linq;
    using System.Collections.Generic;

    using Contracts.Finance;
    using CRM.Data;
    using CRM.Data.Models.Finance;
    using Data.ViewModels.Finance.FinanceInvoice;

    public class FinanceInvoicingServices : IFinanceInvoicingServices
    {
        private ICRMData Data { get; }

        public FinanceInvoicingServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var financeInvoices = this.Data.FinanceInvoices
                .All()
                .Select(f => f.Receiver)
                .ToList();

            return financeInvoices;
        }

        public FinanceInvoiceViewModel FinanceInvoiceInformation(int invoiceId)
        {
            var financeInvoiceInfo = this.Data.FinanceInvoices
                .All()
                .Select(FinanceInvoiceViewModel.FromFinanceInvoice)
                .FirstOrDefault(p => p.Id == invoiceId);

            return financeInvoiceInfo;
        }

        public FinanceInvoiceViewModel FinanceInvoiceDetails(int invoiceId)
        {
            var financeInvoiceIDetails = this.Data.FinanceInvoices
                .All()
                .Select(FinanceInvoiceViewModel.FromFinanceInvoice)
                .FirstOrDefault(t => t.Id == invoiceId);

            return financeInvoiceIDetails;
        }

        public List<FinanceInvoiceViewModel> ReadFinanceInvoices(string searchboxinvoice)
        {
            List<FinanceInvoiceViewModel> readFinanceInvoices;

            if (string.IsNullOrEmpty(searchboxinvoice) || searchboxinvoice == "")
            {
                readFinanceInvoices = this.Data.FinanceInvoices
                .All()
                .Select(FinanceInvoiceViewModel.FromFinanceInvoice)
                .ToList();
            }
            else
            {
                readFinanceInvoices = this.Data.FinanceInvoices
                .All()
                .Select(FinanceInvoiceViewModel.FromFinanceInvoice)
                .Where(p => p.Receiver.Contains(searchboxinvoice))
                .ToList();
            }

            return readFinanceInvoices;
        }

        public FinanceInvoiceViewModel CreateFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice)
        {
            if (givenFinanceInvoice == null)
            {
                return null;
            }

            var newFinanceInvoice = new FinanceInvoice
            {
                NumberOfInvoice = givenFinanceInvoice.NumberOfInvoice,
                Date = givenFinanceInvoice.Date,
                Receiver = givenFinanceInvoice.Receiver,
                SumWithoutDdS = givenFinanceInvoice.SumWithoutDdS,
                SumWithDds = givenFinanceInvoice.SumWithDds
            };

            this.Data.FinanceInvoices.Add(newFinanceInvoice);
            this.Data.SaveChanges();

            givenFinanceInvoice.Id = newFinanceInvoice.Id;

            return givenFinanceInvoice;
        }

        public FinanceInvoiceViewModel UpdateFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice)
        {
            var invoiceFromDb = this.Data.FinanceInvoices
                .All()
              .FirstOrDefault(p => p.Id == givenFinanceInvoice.Id);

            if (givenFinanceInvoice == null || invoiceFromDb == null)
            {
                return givenFinanceInvoice;
            }

            invoiceFromDb.NumberOfInvoice = givenFinanceInvoice.NumberOfInvoice;
            invoiceFromDb.Date = givenFinanceInvoice.Date;
            invoiceFromDb.Receiver = givenFinanceInvoice.Receiver;
            invoiceFromDb.SumWithoutDdS = givenFinanceInvoice.SumWithoutDdS;
            invoiceFromDb.SumWithDds = givenFinanceInvoice.SumWithDds;

            this.Data.SaveChanges();

            return givenFinanceInvoice;
        }

        public FinanceInvoiceViewModel DestroyFinanceInvoice(FinanceInvoiceViewModel givenFinanceInvoice)
        {
            this.Data.FinanceInvoices.Delete(givenFinanceInvoice.Id);
            this.Data.SaveChanges();

            return givenFinanceInvoice;
        }

        public string GetFinanceInvoicById(int invoiceId)
        {
            var invoiceById = this.Data.FinanceInvoices
                .All()
                .Where(t => t.Id == invoiceId)
                .Select(t => t.Receiver.ToString())
                .FirstOrDefault();

            return invoiceById;
        }
    }
}
