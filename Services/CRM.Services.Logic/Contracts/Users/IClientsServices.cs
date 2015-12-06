namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Users.Clients;
    using Data.ViewModels.Users.Contracts;

    public interface IClientsServices : IService
    {
        List<string> GetNames();

        List<DatabaseDataDropdownViewModel> Index();

        ClientViewModel ClientInformation(int clientId);

        string GetTypeOfCompany(int typeId);

        List<ClientViewModel> ReadClients(string searchboxClients);

        ClientViewModel CreateClient(ClientViewModel client);

        ClientViewModel UpdateClient(ClientViewModel client);

        ClientViewModel DestroyClient(ClientViewModel client);
    }
}