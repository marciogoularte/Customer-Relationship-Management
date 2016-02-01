using System.IO;
using System.IO.Compression;
using CRM.Services.Data.ViewModels.Contracts.Contracts;
using CRM.Services.Logic.Contracts.Contractors;
using CRM.Common.Constants;
using Rotativa.Options;

namespace CRM.Web.Areas.Contractors.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Rotativa;
    using Data.Models;
    using Web.Controllers;
    using Web.Common.Base;
    using Services.Logic.Contracts.Users;
    using System.Net;

    [Authorize]
    public class ContractsController : BaseController
    {
        private readonly IContractsServices contracts;

        public ContractsController(IContractsServices contracts)
        {
            this.contracts = contracts;
        }

        [HttpGet]
        public ActionResult AllClientsContracts(int clientId)
        {
            var vm = this.contracts.AllClientsContracts(clientId);
            ViewData["Providers"] = vm;

            return PartialView("_ClientContracts", clientId);
        }

        [HttpGet]
        public ActionResult AllProvidersContracts(int providerId)
        {
            return PartialView("_ProviderContracts", providerId);
        }

        [HttpGet]
        public ActionResult ClientContractInformation(int contractId)
        {
            var contract = this.contracts.ClientContractInformation(contractId);
            var channels = this.contracts.GetChannels(contractId);
            ViewBag.Channels = channels;

            return PartialView("_ClientContractInformation", contract);
        }

        [HttpGet]
        public ActionResult ClientContractDetails(int contractId)
        {
            return View(contractId);
        }

        [HttpGet]
        public ActionResult ProviderContractDetails(int contractId)
        {
            var contract = this.contracts.ProviderContractDetails(contractId);

            return View(contract);
        }

        public ActionResult ClientsContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.contracts.ClientsContractsNames();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProvidersContractsNames([DataSourceRequest]DataSourceRequest request)
        {
            var contractsNames = this.contracts.ProvidersContractsNames();

            return Json(contractsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadClientsContracts([DataSourceRequest] DataSourceRequest request, string searchTerm, int clientId)
        {
            var clientContracts = this.contracts.ReadClientsContracts(searchTerm, clientId);

            return Json(clientContracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadProvidersContracts([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId)
        {
            var providerContracts = this.contracts.ReadProvidersContracts(searchbox, providerId);

            return Json(providerContracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateClientContract([DataSourceRequest]  DataSourceRequest request, ClientContractViewModel contract, int currentClientId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = this.contracts.CreateClientContract(contract, currentClientId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            contract.Id = newContract.Id;

            return Json(new[] { newContract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateProviderContract([DataSourceRequest]  DataSourceRequest request, ProviderContractViewModel contract, int currentProviderId)
        {
            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newContract = this.contracts.CreateProviderContract(contract, currentProviderId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, newContract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            contract.Id = newContract.Id;

            return Json(new[] { newContract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateClientContract([DataSourceRequest] DataSourceRequest request, ClientContractViewModel contract)
        {
            foreach (var propertyName in ModelState.Select(modelError => modelError.Key))
            {
                if (propertyName.Contains("Client"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
                else if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
            }

            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedContract = this.contracts.UpdateClientContract(contract);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedContract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            return Json((new[] { updatedContract }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateProviderContract([DataSourceRequest] DataSourceRequest request, ProviderContractViewModel contract)
        {
            foreach (var propertyName in ModelState.Select(modelError => modelError.Key))
            {
                if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
            }

            if (contract == null || !ModelState.IsValid)
            {
                return Json(new[] { contract }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedContract = this.contracts.UpdateProviderContract(contract);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedContract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            return Json((new[] { updatedContract }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyClientContract([DataSourceRequest] DataSourceRequest request, ClientContractViewModel contract)
        {
            var deletedContract = this.contracts.DestroyClientContract(contract);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, contract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            return Json(new[] { deletedContract }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyProviderContract([DataSourceRequest] DataSourceRequest request, ProviderContractViewModel contract)
        {
            var deletedContract = this.contracts.DestroyProviderContract(contract);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, contract.Id.ToString(), ActivityTargetType.Contract, loggedUserId);

            return Json(new[] { deletedContract }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllClientContractChannels(int clientContractId)
        {
            var result = this.contracts.AllClientContractChannels(clientContractId);

            return PartialView("_ClientContractChannels", result);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public void AddOrRemoveChannelFromClientContract(int clientContractId, int channelId)
        {
            this.contracts.AddOrRemoveChannelFromClientContract(clientContractId, channelId);
        }

        public ActionResult GenerateContractTemplate(int providerId, int contractId)
        {
            var contractsData = this.contracts.GenerateContractTemplate(providerId, contractId);
            var contract = contractsData.Contract;

            ViewBag.InvoiceSum = contractsData.Sum;
            ViewBag.Client = contractsData.Client;
            ViewBag.Provider = contractsData.Provider;
            ViewBag.Channels = contractsData.Channels;
            ViewBag.Invoices = contractsData.Invoices;
            ViewBag.Trds = contractsData.Trds;
            ViewBag.MgSubs = contractsData.MgSubs;
            ViewBag.Cps = contractsData.Cps;
            ViewBag.ClientContractType = contractsData.ClientContractType;

            ViewAsPdf file;

            switch (contractsData.ContractTemplate)
            {
                case ContractTemplate.Box:
                    file = new ViewAsPdf("ContractsTemplates/Box", contract)
                    {
                        FileName = ("Box contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.EbuLa:
                    file = new ViewAsPdf("ContractsTemplates/Ebu_la", contract)
                    {
                        FileName = ("Ebu La contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Ectv:
                    file = new ViewAsPdf("ContractsTemplates/Ectv", contract)
                    {
                        FileName = ("Ectv contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Fashionone:
                    file = new ViewAsPdf("ContractsTemplates/Fashionone", contract)
                    {
                        FileName = ("Fashionone contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Fcw:
                    file = new ViewAsPdf("ContractsTemplates/Fcw", contract)
                    {
                        FileName = ("Fcw contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Fishing:
                    file = new ViewAsPdf("ContractsTemplates/Fishing", contract)
                    {
                        FileName = ("Fishing contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Imagine:
                    file = new ViewAsPdf("ContractsTemplates/Imagine", contract)
                    {
                        FileName = ("Imagine contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Moviestar:
                    file = new ViewAsPdf("ContractsTemplates/Moviestar", contract)
                    {
                        FileName = ("Moviestar contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Roma:
                    file = new ViewAsPdf("ContractsTemplates/Roma", contract)
                    {
                        FileName = ("Roma contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Sct:
                    file = new ViewAsPdf("ContractsTemplates/Sct", contract)
                    {
                        FileName = ("Sct contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.SuperOne:
                    file = new ViewAsPdf("ContractsTemplates/SuperOne", contract)
                    {
                        FileName = ("Super One contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.TheWorld:
                    file = new ViewAsPdf("ContractsTemplates/TheWorld", contract)
                    {
                        FileName = ("The World contract - " + DateTime.Now + ".pdf")
                    };
                    return file;
                case ContractTemplate.Bulsat:
                    file = new ViewAsPdf("ContractsTemplates/Bulsat", contract)
                    {
                        FileName = ("Bulsat VIRGIN XCHANGE contract - " + DateTime.Now + ".pdf")
                    };

                    return file;
            }

            return new EmptyResult();
        }

    }
}