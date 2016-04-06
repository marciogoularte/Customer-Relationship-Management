namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Contracts;

    public interface IClientsServices : IService
    {
        List<string> GetNames();

        List<DatabaseDataDropdownViewModel> Index();

        ClientViewModel ClientInformation(int clientId);

        string GetTypeOfCompany(int TypeOfCompany);

        List<ClientViewModel> ReadClients(string searchboxClients, bool showAll);

        ClientViewModel CreateClient(ClientViewModel client);

        ClientViewModel UpdateClient(ClientViewModel client);

        ClientViewModel DestroyClient(ClientViewModel client);

        List<DatabaseDataDropdownViewModel> GetDealers();
    }
}