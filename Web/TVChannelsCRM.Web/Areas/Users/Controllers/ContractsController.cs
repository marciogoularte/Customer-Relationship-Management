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
    using ViewModels.Providers;
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

            var providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .ToList();

            var vm = providers
                .Select(provider => new ProviderDropdownViewModel
                        {
                            Text = provider.Name,
                            Value = provider.Id
                        })
                .ToList();

            ViewData["Providers"] = vm;

            return PartialView("_ClientContracts", clientId);
        }

        [HttpGet]
        public ActionResult AllProvidersContracts(int providerId)
        {
            return PartialView("_ProviderContracts", providerId);
        }

        [HttpGet]
        public async Task<ActionResult> ClientContractInformation(int contractId)
        {
            var contract = await this.Data.ClientContracts
                .All()
                .Select(ClientContractViewModel.FromClientContract)
                .FirstOrDefaultAsync(p => p.Id == contractId);

            if (contract != null && contract.ProviderId != null)
            {
                try
                {
                    var providerAsInt = int.Parse(contract.ProviderId);

                    var channels = await this.Data.Channels
                        .All()
                        .Where(c => c.ProviderId == providerAsInt)
                        .Select(ChannelViewModel.FromChannel)
                        .ToListAsync();

                    ViewBag.Channels = channels;
                }
                catch (Exception) { }
            }

            return PartialView("_ClientContractInformation", contract);
        }

        [HttpGet]
        public ActionResult ClientContractDetails(int contractId)
        {
            return View(contractId);
        }

        [HttpGet]
        public async Task<ActionResult> ProviderContractDetails(int contractId)
        {
            var contract = await this.Data.ProviderContracts
                .All()
                .Select(ProviderContractViewModel.FromProviderContract)
                .FirstOrDefaultAsync(c => c.Id == contractId);

            return View(contract);
        }

        public ActionResult ClientsContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.Data.ClientContracts
                .All()
                .Select(c => c.TypeOfContract)
                .ToList();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProvidersContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.Data.ProviderContracts
                .All()
                .Select(c => c.TypeOfContract)
                .ToList();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadClientsContracts([DataSourceRequest] DataSourceRequest request, string searchTerm, int clientId)
        {
            List<ClientContractViewModel> contracts;
            if (string.IsNullOrEmpty(searchTerm) || searchTerm == "")
            {
                contracts = this.Data.ClientContracts
                .All()
                .Select(ClientContractViewModel.FromClientContract)
                .Where(c => c.ClientId == clientId)
                .ToList();
            }
            else
            {
                contracts = this.Data.ClientContracts
                .All()
                .Select(ClientContractViewModel.FromClientContract)
                .Where(c => c.TypeOfContract.Contains(searchTerm) && c.ClientId == clientId)
                .ToList();
            }

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadProvidersContracts([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId)
        {
            List<ProviderContractViewModel> contracts;
            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                contracts = this.Data.ProviderContracts
                   .All()
                   .Select(ProviderContractViewModel.FromProviderContract)
                   .Where(c => c.ProviderId == providerId)
                   .ToList();
            }
            else
            {
                contracts = this.Data.ProviderContracts
                   .All()
                   .Select(ProviderContractViewModel.FromProviderContract)
                   .Where(c => c.TypeOfContract.Contains(searchbox) && c.ProviderId == providerId)
                   .ToList();
            }

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateClientContract([DataSourceRequest]  DataSourceRequest request, ClientContractViewModel contract, int currentClientId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = new ClientContract
            {
                StartDate = contract.StartDate,
                TypeOfContract = contract.TypeOfContract,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                ClientId = currentClientId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n",
            };

            if (contract.ProviderId != null)
            {
                newContract.ProviderId = int.Parse(contract.ProviderId);
            }

            this.Data.ClientContracts.Add(newContract);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract);
            contract.Id = newContract.Id;

            return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateProviderContract([DataSourceRequest]  DataSourceRequest request, ProviderContractViewModel contract, int currentProviderId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = new ProviderContract
            {
                StartDate = contract.StartDate,
                TypeOfContract = contract.TypeOfContract,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                ProviderId = currentProviderId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n"
            };

            this.Data.ProviderContracts.Add(newContract);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract);
            contract.Id = newContract.Id;

            return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateClientContract([DataSourceRequest] DataSourceRequest request, ClientContractViewModel contract)
        {
            var contractFromDb = this.Data.ClientContracts
                .All()
                .FirstOrDefault(c => c.Id == contract.Id);

            foreach (var modelError in ModelState)
            {
                var propertyName = modelError.Key;

                if (propertyName.Contains("Client"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
                else if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }

            }

            if (contract == null || !ModelState.IsValid || contractFromDb == null)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            contractFromDb.StartDate = contract.StartDate;
            contractFromDb.TypeOfContract = contract.TypeOfContract;
            contractFromDb.EndDate = contract.EndDate;
            contractFromDb.NoticePeriod = contract.NoticePeriod;
            contractFromDb.BillingStartDate = contract.BillingStartDate;
            contractFromDb.BillingEndDate = contract.BillingEndDate;
            contractFromDb.NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate;
            contractFromDb.NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered;
            contractFromDb.AcceptingReports = contract.AcceptingReports;
            contractFromDb.GoverningLaw = contract.GoverningLaw;
            contractFromDb.CreatedOn = DateTime.Now;
            contractFromDb.Comments = contract.Comments + "\n";
            contractFromDb.ProviderId = int.Parse(contract.ProviderId);

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, contractFromDb.Id.ToString(), ActivityTargetType.Contract);

            return Json((new[] { contract }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProviderContract([DataSourceRequest] DataSourceRequest request, ProviderContractViewModel contract)
        {
            var contractFromDb = this.Data.ProviderContracts
                .All()
                .FirstOrDefault(c => c.Id == contract.Id);

            foreach (var propertyName in ModelState.Select(modelError => modelError.Key))
            {
                if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
            }

            if (contract == null || !ModelState.IsValid || contractFromDb == null)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            contractFromDb.StartDate = contract.StartDate;
            contractFromDb.TypeOfContract = contract.TypeOfContract;
            contractFromDb.EndDate = contract.EndDate;
            contractFromDb.NoticePeriod = contract.NoticePeriod;
            contractFromDb.BillingStartDate = contract.BillingStartDate;
            contractFromDb.BillingEndDate = contract.BillingEndDate;
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

        public JsonResult DestroyClientContract([DataSourceRequest] DataSourceRequest request, ProviderContractViewModel contract)
        {
            this.Data.ClientContracts.Delete(contract.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, contract.Id.ToString(), ActivityTargetType.Contract);

            return Json(new[] { contract }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyProviderContract([DataSourceRequest] DataSourceRequest request, ProviderContractViewModel contract)
        {
            this.Data.ProviderContracts.Delete(contract.Id);
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