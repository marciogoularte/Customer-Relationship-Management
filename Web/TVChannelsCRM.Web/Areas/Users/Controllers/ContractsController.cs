namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Contracts;


    public class ContractsController : BaseController
    {
        public ContractsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllClientsContracts(int clientId)
        {
            return PartialView("_ClientContracts", clientId);
        }

        public ActionResult ClientsContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.Data.Contracts
                .All()
                .Where(c => c.Client != null && c.Client.Id != null && c.Client.AccountManager != null)
                .Select(c => c.Client.AccountManager)
                .ToList();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientsContractsSearch([DataSourceRequest] DataSourceRequest request, string searchbox, int clientId)
        {
            var contracts = this.Data.Contracts
                .All()
                .Select(ContractViewModel.FromContract)
                .Where(c => c.Client.AccountManager.Contains(searchbox) && c.Client.Id == clientId)
                .ToList();

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadClientsContracts([DataSourceRequest] DataSourceRequest request, int clientId)
        {
            var contracts = this.Data.Contracts
                .All()
                .Where(c => c.Client.Id == clientId)
                .Select(ContractViewModel.FromContract)
                .ToList();

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateClientContract([DataSourceRequest]  DataSourceRequest request, ContractViewModel contract, int clientId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = new Contract
            {
                StartDate = contract.StartDate,
                Term = contract.Term,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                StartingDateOfDueDate = contract.StartingDateOfDueDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                ClientId = clientId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n"
            };

            this.Data.Contracts.Add(newContract);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract);
            contract.Id = newContract.Id;

            return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllProvidersContracts(int providerId)
        {
            return PartialView("_ProviderContracts", providerId);
        }

        public ActionResult ProvidersContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.Data.Contracts
                .All()
                .Where(c => c.Provider != null && c.Provider.Id != null && c.Provider.Name != null)
                .Select(c => c.Provider.Name)
                .ToList();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProvidersContractsSearch([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId)
        {
            var contracts = this.Data.Contracts
                .All()
                .Select(ContractViewModel.FromContract)
                .Where(c => c.Provider.Name.Contains(searchbox) && c.Provider.Id == providerId)
                .ToList();

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadProvidersContracts([DataSourceRequest] DataSourceRequest request, int providerId)
        {
            var contracts = this.Data.Contracts
                .All()
                .Where(c => c.Provider.Id == providerId)
                .Select(ContractViewModel.FromContract)
                .ToList();

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateProviderContract([DataSourceRequest]  DataSourceRequest request, ContractViewModel contract, int providerId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = new Contract
            {
                StartDate = contract.StartDate,
                Term = contract.Term,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                StartingDateOfDueDate = contract.StartingDateOfDueDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                ProviderId = providerId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n"
            };

            this.Data.Contracts.Add(newContract);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract);
            contract.Id = newContract.Id;

            return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ContractDetails(int contractId)
        {
            var contract = await this.Data.Contracts
                .All()
                .Select(ContractViewModel.FromContract)
                .FirstOrDefaultAsync(c => c.Id == contractId);

            return View(contract);
        }

        public JsonResult UpdateContract([DataSourceRequest] DataSourceRequest request, ContractViewModel contract)
        {
            var contractFromDb = this.Data.Contracts
                .All()
                  .FirstOrDefault(c => c.Id == contract.Id);

            if (contract == null || !ModelState.IsValid || contractFromDb == null)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            contractFromDb.StartDate = contract.StartDate;
            contractFromDb.Term = contract.Term;
            contractFromDb.EndDate = contract.EndDate;
            contractFromDb.NoticePeriod = contract.NoticePeriod;
            contractFromDb.BillingStartDate = contract.BillingStartDate;
            contractFromDb.BillingEndDate = contract.BillingEndDate;
            contractFromDb.StartingDateOfDueDate = contract.StartingDateOfDueDate;
            contractFromDb.NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate;
            contractFromDb.NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered;
            contractFromDb.AcceptingReports = contract.AcceptingReports;
            contractFromDb.GoverningLaw = contract.GoverningLaw;
            contractFromDb.CreatedOn = DateTime.Now;
            contractFromDb.Comments = contract.Comments + "\n";

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, contractFromDb.Id.ToString(), ActivityTargetType.Contract);

            return Json((new[] { contract }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyContract([DataSourceRequest] DataSourceRequest request, ContractViewModel contract)
        {
            this.Data.Contracts.Delete(contract.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, contract.Id.ToString(), ActivityTargetType.Contract);

            return Json(new[] { contract }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        private void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            // If activities are more than 200 just override the oldest one so will not have more than 200 activities
            if (this.Data.Activities.All().Count() >= 200)
            {
                var activity = this.Data.Activities.All().OrderBy(a => a.CreatedOn).FirstOrDefault();
                activity.UserId = loggedUserId;
                activity.Type = type;
                activity.TargetId = targetId;
                activity.TargetType = targetType;
                activity.CreatedOn = DateTime.Now;
            }
            else
            {
                var activity = new Activity()
                {
                    UserId = loggedUserId,
                    Type = type,
                    TargetId = targetId,
                    TargetType = targetType
                };

                this.Data.Activities.Add(activity);
            }

            this.Data.SaveChanges();
        }
    }
}