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
    using ViewModels.Clients;

    [Authorize]
    public class ClientsController : BaseController
    {
        public ClientsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ClientDetails(int clientId)
        {
            var client = await this.Data.Clients
                .All()
                .Select(ClientViewModel.FromClient)
                .FirstOrDefaultAsync(p => p.Id == clientId);

            return View(client);
        }

        public JsonResult ReadClients([DataSourceRequest] DataSourceRequest request)
        {
            var clients = this.Data.Clients
                .All()
                .Select(ClientViewModel.FromClient)
                .ToList();

            return Json(clients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateClient([DataSourceRequest]  DataSourceRequest request, ClientViewModel client)
        {
            if (client == null || !ModelState.IsValid)
            {
                return Json(new[] { client }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newClient = new Client
            {
                IsActive = client.IsActive,
                Mg = client.Mg,
                IrdCard = client.IrdCard,
                Invoicing = client.Invoicing,
                Currency = client.Currency,
                InvoicesIssued = client.InvoicesIssued,
                PaymentsReceived = client.PaymentsReceived,
                Contract = client.Contract,
                ActiveFrom = client.ActiveFrom,
                ActiveTo = client.ActiveTo,
                DateOfSigning = client.DateOfSigning,
                DateOfExpiring = client.DateOfExpiring
            };

            // Validate given dates from user
            //string date;
            //if (client.ActiveFrom != null)
            //{
            //    try
            //    {
            //        date = client.ActiveFrom.ToString();
            //        newClient.ActiveFrom = DateTime.Parse(date);
            //    }
            //    catch (FormatException e)
            //    {
            //        newClient.ActiveFrom = null;
            //    }
            //}

            //if (client.ActiveTo != null)
            //{
            //    try
            //    {
            //        date = client.ActiveTo.ToString();
            //        newClient.ActiveTo = DateTime.Parse(date);
            //    }
            //    catch (FormatException e)
            //    {
            //        newClient.ActiveTo = null;
            //    }
            //}

            //if (client.DateOfSigning != null)
            //{
            //    try
            //    {
            //        date = client.DateOfSigning.ToString();
            //        newClient.DateOfSigning = DateTime.Parse(date);
            //    }
            //    catch (FormatException e)
            //    {
            //        newClient.DateOfSigning = null;
            //    }
            //}

            //if (client.DateOfExpiring != null)
            //{
            //    try
            //    {
            //        date = client.DateOfExpiring.ToString();
            //        newClient.DateOfExpiring = DateTime.Parse(date);
            //    }
            //    catch (FormatException e)
            //    {
            //        newClient.DateOfExpiring = null;
            //    }
            //}

            this.Data.Clients.Add(newClient);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newClient.Id.ToString(), ActivityTargetType.Client);
            client.Id = newClient.Id;

            return Json(new[] { client }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateClient([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            var clientFromDb = this.Data.Clients.All()
             .FirstOrDefault(p => p.Id == client.Id);

            if (client == null || !ModelState.IsValid || clientFromDb == null)
            {
                return Json(new[] { client }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            clientFromDb.IsActive = client.IsActive;
            clientFromDb.ActiveFrom = client.ActiveFrom;
            clientFromDb.ActiveTo = client.ActiveTo;
            clientFromDb.Mg = client.Mg;
            clientFromDb.IrdCard = client.IrdCard;
            clientFromDb.Invoicing = client.Invoicing;
            clientFromDb.DateOfSigning = client.DateOfSigning;
            clientFromDb.DateOfExpiring = client.DateOfExpiring;
            clientFromDb.Currency = client.Currency;
            clientFromDb.InvoicesIssued = client.InvoicesIssued;
            clientFromDb.PaymentsReceived = client.PaymentsReceived;
            clientFromDb.Contract = client.Contract;

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, clientFromDb.Id.ToString(), ActivityTargetType.Client);

            return Json((new[] { client }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyClient([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            var clientIdToInt = int.Parse(client.Id.ToString());
            this.Data.Clients.Delete(clientIdToInt);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, client.Id.ToString(), ActivityTargetType.Client);

            return Json(new[] { client }, JsonRequestBehavior.AllowGet);
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