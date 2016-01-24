using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Contracts;
using CRM.Services.Data.ViewModels.Contracts.Providers;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface IProvidersServices : IService
    {
        List<string> GetProvidersNames();

        List<DatabaseDataDropdownViewModel> Index();

        ProviderViewModel ProviderInformation(int providerId);

        string GetTypeOfCompany(int TypeOfCompany);

        List<ProviderViewModel> ReadProviders(string searchboxProviders);

        ProviderViewModel CreateProvider(ProviderViewModel provider);

        ProviderViewModel UpdateProvider(ProviderViewModel provider);

        ProviderViewModel DestroyProvider(ProviderViewModel provider);

        string GetProviderName(int providerId);
    }
}