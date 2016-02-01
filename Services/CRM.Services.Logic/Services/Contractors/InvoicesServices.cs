using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CRM.Data;
using CRM.Data.Models;
using CRM.Services.Data.ViewModels.Contracts.Invoices;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
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
                    .ProjectTo<InvoiceViewModel>()
                    .Where(i => i.ClientContractId == contractId && i.IsVisible)
                    .ToList();
            }
            else
            {
                invoices = this.Data.Invoices
                   .All()
                   .ProjectTo<InvoiceViewModel>()
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
                CorrespondencePayment = invoice.CorrespondencePayment,
                AdditionalInformation = invoice.AdditionalInformation,
                FixedMonthlyFee = invoice.FixedMonthlyFee,
                Vat = invoice.Vat,
                ClientContractId = contractId,
                Comments = invoice.Comments,
                IsVisible = invoice.IsVisible
            };

            this.Data.Invoices.Add(newInvoice);
            this.Data.SaveChanges();

            invoice.Id = newInvoice.Id;
            return invoice;
        }

        public InvoiceViewModel InvoiceDetails(int invoiceId)
        {
            var invoice = this.Data.Invoices
                .All()
                .ProjectTo<InvoiceViewModel>()
                .FirstOrDefault(c => c.Id == invoiceId);

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
            invoiceFromDb.Comments = invoice.Comments;
            invoiceFromDb.CorrespondencePayment = invoice.CorrespondencePayment;
            invoiceFromDb.AdditionalInformation = invoice.AdditionalInformation;
            invoiceFromDb.FixedMonthlyFee = invoice.FixedMonthlyFee;
            invoiceFromDb.Vat = invoice.Vat;
            invoiceFromDb.IsVisible = invoice.IsVisible;

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
