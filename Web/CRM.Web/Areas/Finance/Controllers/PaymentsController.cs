namespace CRM.Web.Areas.Finance.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;
    
    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Finance;
    using Services.Data.ViewModels.Finance.Payments;

    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IPaymentsServices payments;
        
        public PaymentsController(IPaymentsServices payments)
        {
            this.payments = payments;
        }

        [HttpGet]
        public ActionResult AllPayments()
        {
            return View("Payments");
        }

        [HttpGet]
        public ActionResult PaymentsInformation(int paymentId)
        {
            var paymentInfo = this.payments.PaymentInformation(paymentId);

            return View("PaymentsInformation", paymentInfo);
        }

        public ActionResult PaymentsNames([DataSourceRequest]DataSourceRequest request)
        {
            var paymentsNames = this.payments.Index();

            return Json(paymentsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPayments([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readPayments = this.payments.ReadPayments(searchTerm, showAll);

            return Json(readPayments.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult CreatePayment([DataSourceRequest]  DataSourceRequest request, PaymentViewModel paymentModel)
        {
            if (paymentModel == null || !ModelState.IsValid)
            {
                return Json(new[] { paymentModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdPayment = this.payments.CreatePayment(paymentModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdPayment.Id.ToString(), ActivityTargetType.Payment, loggedUserId);

            paymentModel.Id = createdPayment.Id;

            return Json(new[] { paymentModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult UpdatePayment([DataSourceRequest] DataSourceRequest request, PaymentViewModel paymentModel)
        {
            if (paymentModel == null || !ModelState.IsValid)
            {
                return Json(new[] { paymentModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedPayment = this.payments.UpdatePayment(paymentModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedPayment.Id.ToString(), ActivityTargetType.Payment, loggedUserId);

            return Json((new[] { paymentModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Financial")]
        public JsonResult DestroyPayment([DataSourceRequest] DataSourceRequest request, PaymentViewModel paymentModel)
        {
            var deletedPayment = this.payments.DestroyPayment(paymentModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedPayment.Id.ToString(), ActivityTargetType.Payment, loggedUserId);

            return Json(new[] { paymentModel }, JsonRequestBehavior.AllowGet);
        }
    }
}