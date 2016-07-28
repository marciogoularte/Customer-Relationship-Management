namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Partners;

    [Authorize]
    public class OperatorsController : Controller
    {
        private readonly IOperatorsServices operators;

        public OperatorsController(IOperatorsServices operators)
        {
            this.operators = operators;
        }

        [HttpGet]
        public ActionResult AllOperators()
        {
            return PartialView("_Operators");
        }

        [HttpGet]
        public ActionResult OperatorsInformation(int operatorId)
        {
            var operatorInfo = operators.OperatorInformation(operatorId);

            return View("OperatorsInformation", operatorInfo);
        }

        public ActionResult OperatorsNames([DataSourceRequest]DataSourceRequest request)
        {
            var operatorsNames = operators.Index();

            return Json(operatorsNames, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ReadOperators([DataSourceRequest] DataSourceRequest request, string searchTerm, bool? showAll)
        {
            var readOperators = (showAll != null) ? (this.operators.ReadOperators(searchTerm, (bool)showAll)) : (this.operators.ReadOperators(searchTerm, false));

            return Json(readOperators.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateOperator([DataSourceRequest]  DataSourceRequest request, OperatorViewModel operatorModel)
        {
            if (operatorModel == null || !ModelState.IsValid)
            {
                return Json(new[] { operatorModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdOperator = this.operators.CreateOperator(operatorModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdOperator.Id.ToString(), ActivityTargetType.Operator, loggedUserId);

            operatorModel.Id = createdOperator.Id;

            return Json(new[] { operatorModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateOperator([DataSourceRequest] DataSourceRequest request, OperatorViewModel operatorModel)
        {
            if (operatorModel == null || !ModelState.IsValid)
            {
                return Json(new[] { operatorModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedOperator = operators.UpdateOperator(operatorModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedOperator.Id.ToString(), ActivityTargetType.Operator, loggedUserId);

            return Json((new[] { operatorModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyOperator([DataSourceRequest] DataSourceRequest request, OperatorViewModel operatorModel)
        {
            var deletedOperator = operators.DestroyOperator(operatorModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedOperator.Id.ToString(), ActivityTargetType.Operator, loggedUserId);

            return Json(new[] { operatorModel }, JsonRequestBehavior.AllowGet);
        }
    }
}