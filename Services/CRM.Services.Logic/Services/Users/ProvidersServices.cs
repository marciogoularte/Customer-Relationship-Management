namespace CRM.Services.Logic.Services.Users
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.Contracts;
    using Data.ViewModels.Users.Providers;
    using Data.ViewModels.Users.TypeOfCompanies;

    public class ProvidersServices : IProvidersServices
    {
        private ICRMData Data { get; set; }

        public ProvidersServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetProvidersNames()
        {
            var providersNames = this.Data.Providers
                .All()
                .Select(p => p.Name)
                .ToList();

            return providersNames;
        }

        public List<DatabaseDataDropdownViewModel> Index()
        {
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

            return vm;
        }

        public ProviderViewModel ProviderInformation(int providerId)
        {
            var provider = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefault(p => p.Id == providerId);

            if (provider == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(provider.TypeId))
            {
                return provider;
            }

            return provider;
        }

        public string GetTypeOfCompany(int typeOfCompanyId)
        {
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == typeOfCompanyId)
                .Select(t => t.Type.ToString())
                .FirstOrDefault();

            return typeOfCompany;

        }

        public List<ProviderViewModel> ReadProviders(string searchboxProviders)
        {
            List<ProviderViewModel> providers;

            if (string.IsNullOrEmpty(searchboxProviders) || searchboxProviders == "")
            {
                providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .ToList();
            }
            else
            {
                providers = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .Where(p => p.Name.Contains(searchboxProviders))
                .ToList();
            }

            return providers;
        }

        public ProviderViewModel CreateProvider(ProviderViewModel provider)
        {
            var newProvider = new Provider()
            {
                Name = provider.Name,
                TypeId = provider.TypeId,
                Uic = provider.Uic,
                Vat = provider.Vat,
                BankAccount = provider.BankAccount,
                ResidenceAndAddress = provider.ResidenceAndAddress,
                NetworkPage = provider.NetworkPage,
                ContactPerson = provider.ContactPerson,
                PhoneNumber = provider.PhoneNumber,
                Email = provider.Email,
                Address = provider.Address,
                Term = provider.Term,
                Cps = provider.Cps,
                Commission = provider.Commission,
                Comments = provider.Comments + "\n",
                ContractTemplate = provider.ContractTemplate,
                Channels = new List<Channel>(),
                Contracts = new List<ProviderContract>(),
                Discussions = new List<Discussion>()
            };

            if (string.IsNullOrEmpty(provider.LogoLink) || provider.LogoLink == "")
            {
                newProvider.LogoLink = "#";
            }
            else
            {
                newProvider.LogoLink = provider.LogoLink;
            }

            this.Data.Providers.Add(newProvider);
            this.Data.SaveChanges();

            provider.Id = newProvider.Id;

            return provider;
        }

        public ProviderViewModel UpdateProvider(ProviderViewModel provider)
        {
            var providerFromDb = this.Data.Providers.All()
                   .FirstOrDefault(p => p.Id == provider.Id);

            providerFromDb.Name = provider.Name;
            providerFromDb.TypeId = provider.TypeId;
            providerFromDb.Uic = provider.Uic;
            providerFromDb.Vat = provider.Vat;
            providerFromDb.BankAccount = provider.BankAccount;
            providerFromDb.ResidenceAndAddress = provider.ResidenceAndAddress;
            providerFromDb.NetworkPage = provider.NetworkPage;
            providerFromDb.ContactPerson = provider.ContactPerson;
            providerFromDb.PhoneNumber = provider.PhoneNumber;
            providerFromDb.Email = provider.Email;
            providerFromDb.Address = provider.Address;
            providerFromDb.Term = provider.Term;
            providerFromDb.Cps = provider.Cps;
            providerFromDb.Commission = provider.Commission;
            providerFromDb.LogoLink = provider.LogoLink;
            providerFromDb.Comments = provider.Comments + "\n";
            providerFromDb.ContractTemplate = provider.ContractTemplate;

            if (string.IsNullOrEmpty(provider.LogoLink) || provider.LogoLink == "")
            {
                providerFromDb.LogoLink = "#";
            }
            else
            {
                providerFromDb.LogoLink = provider.LogoLink;
            }

            this.Data.SaveChanges();

            return provider;
        }

        public ProviderViewModel DestroyProvider(ProviderViewModel provider)
        {
            var providerIdToInt = int.Parse(provider.Id.ToString());
            this.Data.Providers.Delete(providerIdToInt);
            this.Data.SaveChanges();

            return provider;
        }

        public string GetProviderName(int providerId)
        {
            var providerName = this.Data.Providers
                .All()
                .Select(ProviderViewModel.FromProvider)
                .FirstOrDefault(p => p.Id == providerId)
                .Name;

            return providerName;
        }
    }
}
