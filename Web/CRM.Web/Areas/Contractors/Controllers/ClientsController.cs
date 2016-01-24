﻿using CRM.Services.Data.ViewModels.Contracts.Clients;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Web.Areas.Contractors.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Web.Controllers;
    using Services.Logic.Contracts.Users;

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

            ViewData["TypeOfCompanies"] = vm;

            return View(names);
        }

        [HttpGet]
        public ActionResult ClientInformation(int clientId)
        {
            var client =  clients.ClientInformation(clientId);

            if (client == null)
            {
                return PartialView("_ClientInformation", null);
            }

            if (string.IsNullOrEmpty(client.TypeOfCompany))
            {
                return PartialView("_ClientInformation", client);
            }

            var parsedTypeOfCompany = int.Parse(client.TypeOfCompany);
            var typeOfCompany = clients.GetTypeOfCompany(parsedTypeOfCompany);
            ViewBag.TypeOfCompany = typeOfCompany;

            return PartialView("_ClientInformation", client);
        }

        [HttpGet]
        public ActionResult ClientDetails(int clientId)
        {
            return View(clientId);
        }

        public JsonResult ReadClients([DataSourceRequest] DataSourceRequest request, string searchboxClients)
        {
            var readClients = clients.ReadClients(searchboxClients);

            return Json(readClients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

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

        public JsonResult DestroyClient([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            var deletedClient = clients.DestroyClient(client);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedClient.Id.ToString(), ActivityTargetType.Client, loggedUserId);

            return Json(new[] { deletedClient }, JsonRequestBehavior.AllowGet);
        }
    }
}