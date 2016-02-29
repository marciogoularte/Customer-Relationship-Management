namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.Providers;

    public interface IProvidersServices : IService
    {
        List<string> GetProvidersNames();

        List<DatabaseDataDropdownViewModel> Index();

        ProviderViewModel ProviderInformation(int providerId);

        string GetTypeOfCompany(int TypeOfCompany);

        List<ProviderViewModel> ReadProviders(string searchboxProviders, bool showAll);

        ProviderViewModel CreateProvider(ProviderViewModel provider);

        ProviderViewModel UpdateProvider(ProviderViewModel provider);

        ProviderViewModel DestroyProvider(ProviderViewModel provider);

        string GetProviderName(int providerId);
    }
}