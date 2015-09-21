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
            var names = this.Data.Clients
                .All()
                .Select(c => c.Name)
                .ToList();

            return View(names);
        }

        [HttpGet]
        public async Task<ActionResult> ClientInformation(int clientId)
        {
            var provider = await this.Data.Clients
                .All()
                .Select(ClientViewModel.FromClient)
                .FirstOrDefaultAsync(p => p.Id == clientId);

            return PartialView("_ClientInformation", provider);
        }

        [HttpGet]
        public ActionResult ClientDetails(int clientId)
        {
            return View(clientId);
        }

        public JsonResult ReadClients([DataSourceRequest] DataSourceRequest request, string searchboxClients)
        {
            List<ClientViewModel> clients;
                if (string.IsNullOrEmpty(searchboxClients) || searchboxClients == "")
            {
                clients = this.Data.Clients
                    .All()
                    .Select(ClientViewModel.FromClient)
                    .ToList();
            }
            else
            {
                clients = this.Data.Clients
                    .All()
                    .Select(ClientViewModel.FromClient)
                    .Where(c => c.Name.Contains(searchboxClients))
                    .ToList();
            }

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
                Name = client.Name,
                Type = client.Type,
                Eik = client.Eik,
                ResidenceAndAddress = client.ResidenceAndAddress,
                NetworkPage = client.NetworkPage,
                ContactPerson = client.ContactPerson,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                SecondaryAddress = client.SecondaryAddress,
                ActiveCable = client.ActiveCable,
                FixedPhoneService = client.FixedPhoneService,
                AccessToPublicServiceThroughChoiceOperator = client.AccessToPublicServiceThroughChoiceOperator,
                MobileVoiceServicesProvidedThroughNetwork = client.MobileVoiceServicesProvidedThroughNetwork,
                PublicServicesProvidedByWirelessAccess = client.PublicServicesProvidedByWirelessAccess,
                ServicesFixedAccessToInternet = client.ServicesFixedAccessToInternet,
                ServicesMobileAccessToInternet = client.ServicesMobileAccessToInternet,
                ServicesTransmissionData = client.ServicesTransmissionData,
                SpreadingRadioAndTvPrograms = client.SpreadingRadioAndTvPrograms,
                Coverage = client.Coverage,
                CorrespondenceAddress = client.CorrespondenceAddress,
                CorAddress = client.CorAddress,
                PostCode = client.PostCode,
                Management = client.Management,
                ManagementPhone = client.ManagementPhone,
                ManagementEmail = client.ManagementEmail,
                ManagementTeritory = client.ManagementTeritory,
                Finance = client.Finance,
                FinancePhone = client.FinancePhone,
                FinanceEmail = client.FinanceEmail,
                TechnicalName = client.TechnicalName,
                TechnicalPhone = client.TechnicalPhone,
                TechnicalEmail = client.TechnicalEmail,
                Marketing = client.Marketing,
                MarketingPhone = client.MarketingPhone,
                MarketingEmail = client.MarketingEmail,
                Contracts = new List<ClientContract>(),
                Discussions = new List<Discussion>()
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

            clientFromDb.Name = client.Name;
            clientFromDb.Type = client.Type;
            clientFromDb.Eik = client.Eik;
            clientFromDb.ResidenceAndAddress = client.ResidenceAndAddress;
            clientFromDb.NetworkPage = client.NetworkPage;
            clientFromDb.ContactPerson = client.ContactPerson;
            clientFromDb.PhoneNumber = client.PhoneNumber;
            clientFromDb.Email = client.Email;
            clientFromDb.SecondaryAddress = client.SecondaryAddress;
            clientFromDb.ActiveCable = client.ActiveCable;
            clientFromDb.FixedPhoneService = client.FixedPhoneService;
            clientFromDb.AccessToPublicServiceThroughChoiceOperator = client.AccessToPublicServiceThroughChoiceOperator;
            clientFromDb.MobileVoiceServicesProvidedThroughNetwork = client.MobileVoiceServicesProvidedThroughNetwork;
            clientFromDb.PublicServicesProvidedByWirelessAccess = client.PublicServicesProvidedByWirelessAccess;
            clientFromDb.ServicesFixedAccessToInternet = client.ServicesFixedAccessToInternet;
            clientFromDb.ServicesMobileAccessToInternet = client.ServicesMobileAccessToInternet;
            clientFromDb.ServicesTransmissionData = client.ServicesTransmissionData;
            clientFromDb.SpreadingRadioAndTvPrograms = client.SpreadingRadioAndTvPrograms;
            clientFromDb.Coverage = client.Coverage;
            clientFromDb.CorrespondenceAddress = client.CorrespondenceAddress;
            clientFromDb.CorAddress = client.CorAddress;
            clientFromDb.PostCode = client.PostCode;
            clientFromDb.Management = client.Management;
            clientFromDb.ManagementPhone = client.ManagementPhone;
            clientFromDb.ManagementEmail = client.ManagementEmail;
            clientFromDb.ManagementTeritory = client.ManagementTeritory;
            clientFromDb.Finance = client.Finance;
            clientFromDb.FinancePhone = client.FinancePhone;
            clientFromDb.FinanceEmail = client.FinanceEmail;
            clientFromDb.TechnicalName = client.TechnicalName;
            clientFromDb.TechnicalPhone = client.TechnicalPhone;
            clientFromDb.TechnicalEmail = client.TechnicalEmail;
            clientFromDb.Marketing = client.Marketing;
            clientFromDb.MarketingPhone = client.MarketingPhone;
            clientFromDb.MarketingEmail = client.MarketingEmail;

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