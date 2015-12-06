namespace CRM.Services.Logic.Services.Users
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.Invoices;

    public class InvoicesServices : IInvoicesServices
    {
        private ICRMData Data { get; set; }

        public InvoicesServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetContractInvoicesData()
        {
            var invoicesData = this.Data.Invoices
                .All()
                .Select(c => c.MgSubs.ToString())
                .ToList();

            return invoicesData;
        }

        public List<InvoiceViewModel> ReadContractInvoices(string searchbox, int contractId)
        {
            List<InvoiceViewModel> invoices;

            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                invoices = this.Data.Invoices
                    .All()
                    .Select(InvoiceViewModel.FromInvoice)
                    .Where(i => i.ClientContractId == contractId)
                    .ToList();
            }
            else
            {
                invoices = this.Data.Invoices
                   .All()
                   .Select(InvoiceViewModel.FromInvoice)
                   .Where(i => i.ClientContractId == contractId && i.MgSubs.ToString().Contains(searchbox))
                   .ToList();
            }

            return invoices;
        }

        public InvoiceViewModel CreateContractInvoice(InvoiceViewModel invoice, int contractId)
        {
            var newInvoice = new Invoice
            {
                Id = invoice.Id,
                From = invoice.From,
                To = invoice.To,
                MgSubs = invoice.MgSubs,
                Cps = invoice.Cps,
                CreatedOn = DateTime.Now,
                ClientContractId = contractId,
                Comments = invoice.Comments + "\n"
            };

            this.Data.Invoices.Add(newInvoice);
            this.Data.SaveChanges();

            invoice.Id = newInvoice.Id;
            return invoice;
        }

        public async Task<InvoiceViewModel> InvoiceDetails(int invoiceId)
        {
            var invoice = await this.Data.Invoices
                .All()
                .Select(InvoiceViewModel.FromInvoice)
                .FirstOrDefaultAsync(c => c.Id == invoiceId);

            return invoice;
        }

        public InvoiceViewModel UpdateContractInvoice(InvoiceViewModel invoice)
        {
            var invoiceFromDb = this.Data.Invoices
                .All()
              .FirstOrDefault(c => c.Id == invoice.Id);

            invoiceFromDb.From = invoice.From;
            invoiceFromDb.To = invoice.To;
            invoiceFromDb.MgSubs = invoice.MgSubs;
            invoiceFromDb.Cps = invoice.Cps;
            invoiceFromDb.Comments = invoice.Comments + '\n';

            this.Data.SaveChanges();
            
            return invoice;
        }

        public InvoiceViewModel DestroyClientInvoice(InvoiceViewModel invoice)
        {
            this.Data.Invoices.Delete(invoice.Id);
            this.Data.SaveChanges();

            return invoice;
        }
    }
}
