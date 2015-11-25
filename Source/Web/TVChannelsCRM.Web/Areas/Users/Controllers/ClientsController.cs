namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Clients;
    using ViewModels.Contracts;
    using ViewModels.TypeOfCompanies;

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

            var typeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .ToList();

            var vm = typeOfCompanies
                .Select(typeOfCompany => new DatabaseDataDropdownViewModel
                {
                    Text = typeOfCompany.Type,
                    Value = typeOfCompany.Id
                })
                .ToList();

            ViewData["TypeOfCompanies"] = vm;

            return View(names);
        }

        [HttpGet]
        public ActionResult ClientInformation(int clientId)
        {
            var client =  this.Data.Clients
                .All()
                .Select(ClientViewModel.FromClient)
                .FirstOrDefault(p => p.Id == clientId);

            if (client == null)
            {
                return PartialView("_ClientInformation", null);
            }

            if (string.IsNullOrEmpty(client.TypeId))
            {
                return PartialView("_ClientInformation", client);
            }

            var parsedTypeOfCompany = int.Parse(client.TypeId);
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == parsedTypeOfCompany)
                .Select(t => t.Type.ToString())
                .FirstOrDefault();
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
                NameBulgarian = client.NameBulgarian,
                Type = client.TypeId,
                Uic = client.Uic,
                Vat = client.Vat,
                ResidenceAndAddress = client.ResidenceAndAddress,
                ResidenceAndAddressInBulgarian = client.ResidenceAndAddressInBulgarian,
                NetworkPage = client.NetworkPage,
                ContactPerson = client.ContactPerson,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Correspondence = client.Correspondence,
                FixedPhoneService = client.FixedPhoneService,
                MobileVoiceServicesProvidedThroughNetwork = client.MobileVoiceServicesProvidedThroughNetwork,
                InternetSubs = client.InternetSubs,
                ServicesMobileAccessToInternet = client.ServicesMobileAccessToInternet,
                TvSubs = client.TvSubs,
                Coverage = client.Coverage,
                PostCode = client.PostCode,
                Management = client.Management,
                ManagementInBulgarian = client.ManagementInBulgarian,
                ManagementPhone = client.ManagementPhone,
                ManagementEmail = client.ManagementEmail,
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
                Discussions = new List<Discussion>(),
                DealerPhone = client.DealerPhone,
                DealerEmail = client.DealerEmail,
                DealerName = client.DealerName,
                WantToReceiveEpg = client.WantToReceiveEpg,
                WantToReceiveNews = client.WantToReceiveNews
            };

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
            clientFromDb.NameBulgarian = client.NameBulgarian;
            clientFromDb.Type = client.TypeId;
            clientFromDb.Uic = client.Uic;
            clientFromDb.Vat = client.Vat;
            clientFromDb.ResidenceAndAddress = client.ResidenceAndAddress;
            clientFromDb.ResidenceAndAddressInBulgarian = client.ResidenceAndAddressInBulgarian;
            clientFromDb.NetworkPage = client.NetworkPage;
            clientFromDb.ContactPerson = client.ContactPerson;
            clientFromDb.PhoneNumber = client.PhoneNumber;
            clientFromDb.Email = client.Email;
            clientFromDb.Correspondence = client.Correspondence;
            clientFromDb.FixedPhoneService = client.FixedPhoneService;
            clientFromDb.MobileVoiceServicesProvidedThroughNetwork = client.MobileVoiceServicesProvidedThroughNetwork;
            clientFromDb.InternetSubs = client.InternetSubs;
            clientFromDb.ServicesMobileAccessToInternet = client.ServicesMobileAccessToInternet;
            clientFromDb.TvSubs = client.TvSubs;
            clientFromDb.Coverage = client.Coverage;
            clientFromDb.PostCode = client.PostCode;
            clientFromDb.Management = client.Management;
            clientFromDb.ManagementInBulgarian = client.ManagementInBulgarian;
            clientFromDb.ManagementPhone = client.ManagementPhone;
            clientFromDb.ManagementEmail = client.ManagementEmail;
            clientFromDb.Finance = client.Finance;
            clientFromDb.FinancePhone = client.FinancePhone;
            clientFromDb.FinanceEmail = client.FinanceEmail;
            clientFromDb.TechnicalName = client.TechnicalName;
            clientFromDb.TechnicalPhone = client.TechnicalPhone;
            clientFromDb.TechnicalEmail = client.TechnicalEmail;
            clientFromDb.Marketing = client.Marketing;
            clientFromDb.MarketingPhone = client.MarketingPhone;
            clientFromDb.MarketingEmail = client.MarketingEmail;
            clientFromDb.DealerName = client.DealerName;
            clientFromDb.DealerEmail = client.DealerEmail;
            clientFromDb.DealerPhone = client.DealerPhone;
            clientFromDb.WantToReceiveNews = client.WantToReceiveNews;
            clientFromDb.WantToReceiveEpg = client.WantToReceiveEpg;

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
    }
}