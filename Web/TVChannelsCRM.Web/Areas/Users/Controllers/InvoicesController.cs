namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Invoices;

    public class InvoicesController : BaseController
    {
        public InvoicesController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllContractInvoices(int contractId)
        {
            return PartialView("_ContractInvoices", contractId);
        }

        public ActionResult GetContractInvoicesData([DataSourceRequest]DataSourceRequest request)
        {
            var invoicesData = this.Data.Invoices
                .All()
                //.Where(i => i.ClientContract != null)
                .Select(c => c.MgSubs.ToString())
                .ToList();

            return Json(invoicesData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadContractInvoices([DataSourceRequest] DataSourceRequest request, string searchbox, int contractId)
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

            return Json(invoices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateContractInvoice([DataSourceRequest]  DataSourceRequest request, InvoiceViewModel invoice, int contractId)
        {
            if (invoice == null || !ModelState.IsValid)
            {
                return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

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

            this.CreateActivity(ActivityType.Create, newInvoice.Id.ToString(), ActivityTargetType.Invoice);
            invoice.Id = newInvoice.Id;

            return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> InvoiceDetails(int invoiceId)
        {
            var invoice = await this.Data.Invoices
                .All()
                .Select(InvoiceViewModel.FromInvoice)
                .FirstOrDefaultAsync(c => c.Id == invoiceId);

            return View(invoice);
        }

        public JsonResult UpdateContractInvoice([DataSourceRequest] DataSourceRequest request, InvoiceViewModel invoice)
        {
            var invoiceFromDb = this.Data.Invoices
                .All()
              .FirstOrDefault(c => c.Id == invoice.Id);

            if (invoice == null || !ModelState.IsValid || invoiceFromDb == null)
            {
                return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            invoiceFromDb.From = invoice.From;
            invoiceFromDb.To = invoice.To;
            invoiceFromDb.MgSubs = invoice.MgSubs;
            invoiceFromDb.Cps = invoice.Cps;
            invoiceFromDb.Comments = invoice.Comments + '\n';

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, invoiceFromDb.Id.ToString(), ActivityTargetType.Invoice);

            return Json((new[] { invoice }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyClientInvoice([DataSourceRequest] DataSourceRequest request, InvoiceViewModel invoice)
        {
            this.Data.Invoices.Delete(invoice.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, invoice.Id.ToString(), ActivityTargetType.Invoice);

            return Json(new[] { invoice }, JsonRequestBehavior.AllowGet);
        }
    }
}