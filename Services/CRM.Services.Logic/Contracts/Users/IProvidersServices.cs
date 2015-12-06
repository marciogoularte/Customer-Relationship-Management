namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Users.Contracts;
    using Data.ViewModels.Users.Providers;

    public interface IProvidersServices : IService
    {
        List<string> GetProvidersNames();

        List<DatabaseDataDropdownViewModel> Index();

        ProviderViewModel ProviderInformation(int providerId);

        string GetTypeOfCompany(int typeOfCompanyId);

        List<ProviderViewModel> ReadProviders(string searchboxProviders);

        ProviderViewModel CreateProvider(ProviderViewModel provider);

        ProviderViewModel UpdateProvider(ProviderViewModel provider);

        ProviderViewModel DestroyProvider(ProviderViewModel provider);

        string GetProviderName(int providerId);
    }
}