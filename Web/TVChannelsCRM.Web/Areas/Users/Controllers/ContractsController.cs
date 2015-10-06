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
<<<<<<< HEAD
=======
    using Microsoft.AspNet.Identity;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

    using Data;
    using Data.Models;
    using Web.Controllers;
<<<<<<< HEAD
    using ViewModels.Trds;
    using ViewModels.Clients;
    using ViewModels.Invoices;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
=======

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
            var providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .ToList();

            var vm = providers
<<<<<<< HEAD
                .Select(provider => new DatabaseDataDropdownViewModel
=======
                .Select(provider => new ProviderDropdownViewModel
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
                        .Where(c => c.ClientContractId == contractId)
                     //   .Where(c => c.ProviderId == providerAsInt)
=======
                        .Where(c => c.ProviderId == providerAsInt)
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
                Tier = contract.Tier,
                ClientId = currentClientId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n",
                Channels = new List<Channel>()
=======
                ClientId = currentClientId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments + "\n",
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
=======
                NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered,
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
            contractFromDb.Tier = contract.Tier;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
=======
            contractFromDb.NumberOfDaysToBeConsidered = contract.NumberOfDaysToBeConsidered;
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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

<<<<<<< HEAD
        public ActionResult AllClientContractChannels(int clientContractId)
        {
            var clientContractProviderId = this.Data.ClientContracts
                .All()
                .Where(c => c.Id == clientContractId)
                .FirstOrDefault(c => c.Id == clientContractId)
                .ProviderId;

            var providerChannels = this.Data.Channels
                .All()
                .Select(ChannelViewModel.FromChannel)
                .Where(c => c.ProviderId == clientContractProviderId)
                .ToList();

            var clientContractChannels = this.Data.Channels
                .All()
                .Select(ChannelViewModel.FromChannel)
                .Where(c => c.ClientContractId == clientContractId)
                .ToList();

            var indexesOfElementForRemove = new List<int>();
            foreach (var channel in clientContractChannels)
            {
                indexesOfElementForRemove.AddRange(
                    from providerChannel in providerChannels 
                    where providerChannel.Id == channel.Id 
                    select providerChannels
                        .FindIndex(p => p.Id == channel.Id));
            }

            foreach (var index in indexesOfElementForRemove)
            {
                providerChannels.RemoveAt(index);
            }

            var result = new ClientContractChannelViewModel()
            {
                ProviderChannels = providerChannels,
                ClientContractChannels = clientContractChannels,
                ClientContractId = clientContractId
            };

            return PartialView("_ClientContractChannels", result);
        }

        public void AddOrRemoveChannelFromClientContract(int clientContractId, int channelId)
        {
            var contract = this.Data.ClientContracts
                .GetById(clientContractId);
            var containsIt = false;

            foreach (var contractChannel in contract.Channels.Where(contractChannel => contractChannel.Id == channelId))
            {
                containsIt = true;
            }

            var channel = this.Data.Channels
                .All()
                .FirstOrDefault(c => c.Id == channelId);

            if (containsIt)
            {
                contract.Channels.Remove(channel);
            }
            else
            {
                contract.Channels.Add(channel);
            }

            this.Data.SaveChanges();
        }

        public async Task<ActionResult> GenerateContractTemplate(int providerId, int contractId)
        {
            var contract = await this.Data.ClientContracts
                .All()
                .Select(ClientContractViewModel.FromClientContract)
                .FirstOrDefaultAsync(c => c.Id == contractId);

            var contractTemplate = this.Data.Providers
                .GetById(providerId).ContractTemplate;

            var client = await this.Data.Clients
                .All()
                .Select(ClientViewModel.FromClient)
                .FirstOrDefaultAsync(c => c.Id == contract.ClientId);

            var provider = await this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefaultAsync(p => p.Id == providerId);

            var invoices = await this.Data.Invoices
                .All()
                .Select(InvoiceViewModel.FromInvoice)
                .Where(i => i.ClientContractId == contract.Id)
                .ToListAsync();

            var channels = await this.Data.Channels
                .All()
                .Select(ChannelViewModel.FromChannel)
                .Where(c => c.ClientContractId == contract.Id)
                .ToListAsync();

            var trds = await this.Data.Trds
                .All()
                .Select(TrdViewModel.FromTrd)
                .Where(t => t.ClientId == contract.ClientId)
                .ToListAsync();

            var totalMgSubs = 0;
            var totalCps = 0;
            foreach (var invoice in invoices)
            {
                var mgSubsAsInt = int.Parse(invoice.MgSubs);
                var cpsInt = int.Parse(invoice.MgSubs);

                totalMgSubs += mgSubsAsInt;
                totalCps += cpsInt;
            }

            var sum = 0;
            if (invoices.Any())
            {
                foreach (var invoice in invoices)
                {
                    var msSubsAsDouble = int.Parse(invoice.MgSubs);
                    var cpsAsDouble = int.Parse(invoice.Cps);
                    sum += (msSubsAsDouble*cpsAsDouble);
                }
            }

            ViewBag.InvoiceSum = sum;
            ViewBag.Client = client;
            ViewBag.Provider = provider;
            ViewBag.Channels = channels;
            ViewBag.Invoices = invoices;
            ViewBag.Trds = trds;
            ViewBag.MgSubs = totalMgSubs;
            ViewBag.Cps = totalCps;

            switch (contractTemplate)
            {
                case ContractTemplate.Box:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Box", contract) { FileName = ("Box contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.EbuLa:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Ebu_la", contract) { FileName = ("Ebu La contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Ectv:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Ectv", contract) { FileName = ("Ectv contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Fashionone:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Fashionone", contract) { FileName = ("Fashionone contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Fcw:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Fcw", contract) { FileName = ("Fcw contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Fishing:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Fishing", contract) { FileName = ("Fishing contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Imagine:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Imagine", contract) { FileName = ("Imagine contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.MixPack:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/MixPack", contract) { FileName = ("Mix Pack contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.MovieSels:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/MovieSels", contract) { FileName = ("Movie Sels contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Moviestar:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Moviestar", contract) { FileName = ("Moviestar contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Roma:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Roma", contract) { FileName = ("Roma contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.Sct:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/Sct", contract) { FileName = ("Sct contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.SuperOne:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/SuperOne", contract) { FileName = ("Super One contract - " + DateTime.Now + ".pdf") };
                    break;
                case ContractTemplate.TheWorld:
                    return new Rotativa.ViewAsPdf("ContractsTemplates/TheWorld", contract) { FileName = ("The World contract - " + DateTime.Now + ".pdf") };
                    break;
            }

            return new EmptyResult();
=======
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
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        }
    }
}