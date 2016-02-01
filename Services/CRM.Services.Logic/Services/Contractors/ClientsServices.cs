namespace CRM.Services.Logic.Services.Contractors
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Contractors;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.TypeOfCompanies;
    
    public class ClientsServices : IClientsServices
    {
        private ICRMData Data { get; set; }

        public ClientsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetNames()
        {
            var names = this.Data.Clients
                .All()
                .Select(c => c.Name)
                .ToList();

            return names;
        }

        public List<DatabaseDataDropdownViewModel> Index()
        {
            var types = this.Data.TypeOfCompanies
                .All()
                .ProjectTo<TypeOfCompanyViewModel>()
                .ToList();

            var vm = types
                .Select(typeOfCompany => new DatabaseDataDropdownViewModel
                {
                    Text = typeOfCompany.Type,
                    Value = typeOfCompany.Id
                })
                .ToList();

            return vm;
        }

        public ClientViewModel ClientInformation(int clientId)
        {
            var client = this.Data.Clients
                .All()
                .ProjectTo<ClientViewModel>()
                .FirstOrDefault(p => p.Id == clientId);

            return client;
        }

        public string GetTypeOfCompany(int typeId)
        {
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == typeId)
                .Select(t => t.Type.ToString())
                .FirstOrDefault();

            return typeOfCompany;
        }

        public List<ClientViewModel> ReadClients(string searchboxClients)
        {
            List<ClientViewModel> clients;

            if (string.IsNullOrEmpty(searchboxClients) || searchboxClients == "")
            {
                clients = this.Data.Clients
                    .All()
                    .Where(c => c.IsVisible)
                    .ProjectTo<ClientViewModel>()
                    .ToList();
            }
            else
            {
                clients = this.Data.Clients
                    .All()
                    .ProjectTo<ClientViewModel>()
                    .Where(c => c.Name.Contains(searchboxClients))
                    .ToList();
            }

            return clients;
        }

        public ClientViewModel CreateClient(ClientViewModel client)
        {
            var newClient = new Client
            {
                Name = client.Name,
                NameBulgarian = client.NameBulgarian,
                TypeOfCompany = client.TypeOfCompany,
                Uic = client.Uic,
                Vat = client.Vat,
                ResidenceAndAddress = client.ResidenceAndAddress,
                ResidenceAndAddressInBulgarian = client.ResidenceAndAddressInBulgarian,
                Region = client.Region,
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
                WantToReceiveNews = client.WantToReceiveNews,
                IsVisible = client.IsVisible
            };

            var marketingOperator = new Operator()
            {
                Name = client.Marketing,
                PhoneNumber = client.MarketingPhone,
                Email = client.MarketingEmail
            };

            this.Data.Operators.Add(marketingOperator);
            this.Data.Clients.Add(newClient);

            this.Data.SaveChanges();

            client.Id = newClient.Id;

            return client;
        }

        public ClientViewModel UpdateClient(ClientViewModel client)
        {
            var clientFromDb = this.Data.Clients.All()
             .FirstOrDefault(p => p.Id == client.Id);

            clientFromDb.Name = client.Name;
            clientFromDb.NameBulgarian = client.NameBulgarian;
            clientFromDb.TypeOfCompany = client.TypeOfCompany;
            clientFromDb.Uic = client.Uic;
            clientFromDb.Vat = client.Vat;
            clientFromDb.ResidenceAndAddress = client.ResidenceAndAddress;
            clientFromDb.ResidenceAndAddressInBulgarian = client.ResidenceAndAddressInBulgarian;
            clientFromDb.Region = client.Region;
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
            clientFromDb.IsVisible = client.IsVisible;

            this.Data.Clients.SaveChanges();

            return client;
        }

        public ClientViewModel DestroyClient(ClientViewModel client)
        {
            var clientIdToInt = int.Parse(client.Id.ToString());

            this.Data.Clients.Delete(clientIdToInt);
            this.Data.Clients.SaveChanges();

            return client;
        }
    }
}