namespace CRM.Web.Areas.Contractors.Controllers
{
    using System.Web.Mvc;

    using Rotativa;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Common.Base;
    using Data.Models;
    using Web.Controllers;
    using Services.Logic.Contracts.Contractors;
    using Services.Data.ViewModels.Contracts.Invoices;
    using System;
    [Authorize]
    public class InvoicesController : BaseController
    {
        private readonly IInvoicesServices invoices;

        public InvoicesController(IInvoicesServices invoices)
        {
            this.invoices = invoices;
        }

        [HttpGet]
        public ActionResult AllContractInvoices(int contractId)
        {
            return PartialView("_ContractInvoices", contractId);
        }

        public ActionResult GetContractInvoicesData([DataSourceRequest]DataSourceRequest request)
        {
            var invoicesData = this.invoices.GetContractInvoicesData();

            return Json(invoicesData, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult InvoiceIsPaid(int invoiceId)
        {
            this.invoices.InvoiceIsPaid(invoiceId);

            return RedirectToAction("InvoiceDetails", new { invoiceId });
        }

        public ActionResult ReadContractInvoices([DataSourceRequest] DataSourceRequest request, string searchbox, int contractId, bool? showAll)
        {
            var readInvoices = (showAll != null) ? (this.invoices.ReadContractInvoices(searchbox, contractId, (bool)showAll)) : (this.invoices.ReadContractInvoices(searchbox, contractId, false));

            return Json(readInvoices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateContractInvoice([DataSourceRequest]  DataSourceRequest request, InvoiceViewModel invoice, int contractId)
        {
            if (invoice.From != null)
            {
                invoice.From = DateTime.Parse(invoice.From.ToString());
            }
            if (invoice.To != null)
            {
                invoice.To = DateTime.Parse(invoice.To.ToString());
            }
            if (invoice.CorrespondencePayment != null)
            {
                invoice.CorrespondencePayment = DateTime.Parse(invoice.CorrespondencePayment.ToString());
            }

            if (invoice == null)
            {
                return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdInvoice = this.invoices.CreateContractInvoice(invoice, contractId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdInvoice.Id.ToString(), ActivityTargetType.Invoice, loggedUserId);

            invoice.Id = createdInvoice.Id;

            return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InvoiceDetails(int invoiceId)
        {
            var invoice = this.invoices.InvoiceDetails(invoiceId);

            return View(invoice);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateContractInvoice([DataSourceRequest] DataSourceRequest request, InvoiceViewModel invoice)
        {
            if (invoice == null || !ModelState.IsValid)
            {
                return Json(new[] { invoice }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedInvoice = this.invoices.UpdateContractInvoice(invoice);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedInvoice.Id.ToString(), ActivityTargetType.Invoice, loggedUserId);

            invoice.Id = updatedInvoice.Id;

            return Json((new[] { invoice }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyClientInvoice([DataSourceRequest] DataSourceRequest request, InvoiceViewModel invoice)
        {
            var deletedInvoice = this.invoices.DestroyClientInvoice(invoice);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedInvoice.Id.ToString(), ActivityTargetType.Invoice, loggedUserId);

            invoice.Id = deletedInvoice.Id;

            return Json(new[] { invoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerateInvoice(int id, string type)
        {
            //return View("InvoicesTemplates/invoice-en");
            var invoiceDetails = this.invoices.InvoiceDetails(id);
            ViewAsPdf file;

            if (type == "bg")
            {
                file = new ViewAsPdf("InvoicesTemplates/invoice-bg", invoiceDetails)
                {
                    FileName = ("Invoice BG - " + DateTime.Now + ".pdf")
                };
            }
            else
            {
                file = new ViewAsPdf("InvoicesTemplates/invoice-en", invoiceDetails)
                {
                    FileName = ("Invoice EN - " + DateTime.Now + ".pdf")
                };
            }

            return file;
        }
    }
}