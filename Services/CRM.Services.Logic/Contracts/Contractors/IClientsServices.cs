using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Clients;
using CRM.Services.Data.ViewModels.Contracts.Contracts;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface IClientsServices : IService
    {
        List<string> GetNames();

        List<DatabaseDataDropdownViewModel> Index();

        ClientViewModel ClientInformation(int clientId);

        string GetTypeOfCompany(int TypeOfCompany);

        List<ClientViewModel> ReadClients(string searchboxClients);

        ClientViewModel CreateClient(ClientViewModel client);

        ClientViewModel UpdateClient(ClientViewModel client);

        ClientViewModel DestroyClient(ClientViewModel client);
    }
}