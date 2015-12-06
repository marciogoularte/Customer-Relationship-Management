namespace CRM.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Web.Controllers;
    using Services.Logic.Contracts.Administration;

    [Authorize(Roles = "Admin")]
    public class DeletedItemsController : BaseController
    {
        private readonly IDeletedItemsServices deletedItems;

        public DeletedItemsController(IDeletedItemsServices deletedItems)
        {
            this.deletedItems = deletedItems;
        }

        public ActionResult DeletedClients()
        {
            var clientsNames = this.deletedItems.DeletedClients();

            return View("DeletedClients", clientsNames);
        }

        public ActionResult DeletedProviders()
        {
            var providersNames = this.deletedItems.DeletedProviders();

            return View("DeletedProviders", providersNames);
        }

        public ActionResult DeletedChannels()
        {
            var channelsNames = this.deletedItems.DeletedChannels();

            return View("DeletedChannels", channelsNames);
        }

        public ActionResult DeletedClientContracts()
        {
            var contractsNames = this.deletedItems.DeletedClientContracts();

            return View("DeletedClientContracts", contractsNames);
        }

        public ActionResult DeletedProviderContracts()
        {
            var contractsNames = this.deletedItems.DeletedProviderContracts();

            return View("DeletedProviderContracts", contractsNames);
        }

        public ActionResult DeletedDiscussions()
        {
            var discussionsSubjects = this.deletedItems.DeletedDiscussions();

            return View("DeletedDiscussions", discussionsSubjects);
        }

        public ActionResult DeletedInvoices()
        {
            var mgSubs = this.deletedItems.DeletedInvoices();

            return View("DeletedInvoices", mgSubs);
        }

        public JsonResult ReadDeletedClients([DataSourceRequest] DataSourceRequest request, string searchboxDeletedClients)
        {
            var clients = this.deletedItems.ReadDeletedClients(searchboxDeletedClients);

            return Json(clients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedProviders([DataSourceRequest] DataSourceRequest request, string searchboxDeletedProviders)
        {
            var providers = this.deletedItems.ReadDeletedProviders(searchboxDeletedProviders);

            return Json(providers.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedChannels([DataSourceRequest] DataSourceRequest request, string searchboxDeletedChannels)
        {
            var channels = this.deletedItems.ReadDeletedChannels(searchboxDeletedChannels);

            return Json(channels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedClientContracts([DataSourceRequest] DataSourceRequest request, string searchboxDeletedContracts)
        {
            var contracts = this.deletedItems.ReadDeletedClientContracts(searchboxDeletedContracts);

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedProviderContracts([DataSourceRequest] DataSourceRequest request, string searchboxDeletedContracts)
        {
            var contracts = this.deletedItems.ReadDeletedProviderContracts(searchboxDeletedContracts);

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedDiscussions([DataSourceRequest] DataSourceRequest request, string searchboxDeletedDiscussions)
        {
            var discussions = this.deletedItems.ReadDeletedDiscussions(searchboxDeletedDiscussions);

            return Json(discussions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedInvoices([DataSourceRequest] DataSourceRequest request, string searchboxDeletedInvoices)
        {
            var invoices = this.deletedItems.ReadDeletedInvoices(searchboxDeletedInvoices);

            return Json(invoices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRestoreClient(int clientId)
        {
            this.deletedItems.ConfirmRestoreClient(clientId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, clientId.ToString(), ActivityTargetType.Client, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreProvider(int providerId)
        {
            this.deletedItems.ConfirmRestoreProvider(providerId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, providerId.ToString(), ActivityTargetType.Provider, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreChannel(int channelId)
        {
            this.deletedItems.ConfirmRestoreChannel(channelId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, channelId.ToString(), ActivityTargetType.Channel, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreClientContract(int contractId)
        {
            this.deletedItems.ConfirmRestoreClientContract(contractId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, contractId.ToString(), ActivityTargetType.Contract, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreProviderContract(int contractId)
        {
            this.deletedItems.ConfirmRestoreProviderContract(contractId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, contractId.ToString(), ActivityTargetType.Contract, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreDiscussion(int discussionId)
        {
            this.deletedItems.ConfirmRestoreDiscussion(discussionId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, discussionId.ToString(), ActivityTargetType.Discussion, loggedUserId);

            return new EmptyResult();
        }

        public ActionResult ConfirmRestoreInvoice(int invoiceId)
        {
            this.deletedItems.ConfirmRestoreInvoice(invoiceId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Restore, invoiceId.ToString(), ActivityTargetType.Invoice, loggedUserId);

            return new EmptyResult();
        }
    }
}