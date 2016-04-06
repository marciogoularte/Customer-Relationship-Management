namespace CRM.Web.Areas.Contractors.Controllers
{
    using System;
    using System.Web.Mvc;
    
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Web.Controllers;
    using Services.Logic.Contracts.Contractors;
    using Services.Data.ViewModels.Contracts.Clients;

    [Authorize]
    public class ClientsController : BaseController
    {
        private readonly IClientsServices clients;

        public ClientsController(IClientsServices clients)
        {
            this.clients = clients;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var names = clients.GetNames();
            var vm = clients.Index();
            var dealerUsers = clients.GetDealers();

            ViewData["TypeOfCompanies"] = vm;
            ViewData["DealerUsers"] = dealerUsers;

            return View(names);
        }

        [HttpGet]
        public ActionResult ClientInformation(int clientId)
        {
            var client = clients.ClientInformation(clientId);

            if (client == null)
            {
                return PartialView("_ClientInformation", null);
            }

            if (string.IsNullOrEmpty(client.TypeOfCompany))
            {
                return PartialView("_ClientInformation", client);
            }

            string typeOfCompany;
            try
            {
                var parsedTypeOfCompany = int.Parse(client.TypeOfCompany);
                typeOfCompany = clients.GetTypeOfCompany(parsedTypeOfCompany);
            }
            catch (Exception)
            {
                typeOfCompany = client.TypeOfCompany;
            }

            ViewBag.TypeOfCompany = typeOfCompany;

            return PartialView("_ClientInformation", client);
        }

        [HttpGet]
        public ActionResult ClientDetails(int clientId)
        {
            return View(clientId);
        }

        public JsonResult ReadClients([DataSourceRequest] DataSourceRequest request, string searchboxClients, bool? showAll)
        {
            var readClients = (showAll != null) ? (this.clients.ReadClients(searchboxClients, (bool)showAll)) : (this.clients.ReadClients(searchboxClients, false));

            return Json(readClients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateClient([DataSourceRequest]  DataSourceRequest request, ClientViewModel client)
        {
            if (client == null || !ModelState.IsValid)
            {
                return Json(new[] { client }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdClient = clients.CreateClient(client);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdClient.Id.ToString(), ActivityTargetType.Client, loggedUserId);

            return Json(new[] { createdClient }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateClient([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            if (client == null || !ModelState.IsValid)
            {
                return Json(new[] { client }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedClient = clients.UpdateClient(client);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedClient.Id.ToString(), ActivityTargetType.Client, loggedUserId);

            return Json((new[] { updatedClient }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyClient([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            var deletedClient = clients.DestroyClient(client);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedClient.Id.ToString(), ActivityTargetType.Client, loggedUserId);

            return Json(new[] { deletedClient }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ShowHideRecords(bool isChecked)
        //{
        //    TempData["ShowAll"] = isChecked;

        //    this.ReadClients(new DataSourceRequest()
        //    {
        //        Aggregates = new List<AggregateDescriptor>(),
        //        Filters = new List<IFilterDescriptor>(),
        //        Groups = new List<GroupDescriptor>(),
        //        Page = 1,
        //        PageSize = 10,
        //        Sorts = new List<SortDescriptor>()
        //    }, null);

        //    return new EmptyResult();
        //}
    }
}