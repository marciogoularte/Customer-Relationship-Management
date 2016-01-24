namespace CRM.Web.Areas.Finance.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Finance;
    using Services.Data.ViewModels.Finance.FinanceInvoice;

    [Authorize]
    public class FinanceInvoicesController : Controller
    {
        private readonly IFinanceInvoicingServices financeInvoices;

        public FinanceInvoicesController(IFinanceInvoicingServices financeInvoices)
        {
            this.financeInvoices = financeInvoices;
        }

        [HttpGet]
        public ActionResult AllFinanceInvoices()
        {
            return View("FinanceInvoices");
        }

        [HttpGet]
        public ActionResult FinanceInvoicesInformation(int financeInvoiceId)
        {
            var financeInvoiceInfo = this.financeInvoices.FinanceInvoiceInformation(financeInvoiceId);

            return View("FinanceInvoicesInformation", financeInvoiceInfo);
        }

        public ActionResult FinanceInvoicesNames([DataSourceRequest]DataSourceRequest request)
        {
            var financeInvoicesNames = this.financeInvoices.Index();

            return Json(financeInvoicesNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadFinanceInvoices([DataSourceRequest] DataSourceRequest request, string searchTerm)
        {
            var readFinanceInvoices = this.financeInvoices.ReadFinanceInvoices(searchTerm);

            return Json(readFinanceInvoices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateFinanceInvoice([DataSourceRequest]  DataSourceRequest request, FinanceInvoiceViewModel financeInvoiceModel)
        {
            if (financeInvoiceModel == null || !ModelState.IsValid)
            {
                return Json(new[] { financeInvoiceModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdFinanceInvoice = this.financeInvoices.CreateFinanceInvoice(financeInvoiceModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdFinanceInvoice.Id.ToString(), ActivityTargetType.FinanceInvoice, loggedUserId);

            financeInvoiceModel.Id = createdFinanceInvoice.Id;

            return Json(new[] { financeInvoiceModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateFinanceInvoice([DataSourceRequest] DataSourceRequest request, FinanceInvoiceViewModel financeInvoiceModel)
        {
            if (financeInvoiceModel == null || !ModelState.IsValid)
            {
                return Json(new[] { financeInvoiceModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedFinanceInvoice = this.financeInvoices.UpdateFinanceInvoice(financeInvoiceModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedFinanceInvoice.Id.ToString(), ActivityTargetType.FinanceInvoice, loggedUserId);

            return Json((new[] { financeInvoiceModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyFinanceInvoice([DataSourceRequest] DataSourceRequest request, FinanceInvoiceViewModel financeInvoiceModel)
        {
            var deletedFinanceInvoice = this.financeInvoices.DestroyFinanceInvoice(financeInvoiceModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedFinanceInvoice.Id.ToString(), ActivityTargetType.FinanceInvoice, loggedUserId);

            return Json(new[] { financeInvoiceModel }, JsonRequestBehavior.AllowGet);
        }
    }
}