using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CRM.Data;
using CRM.Data.Models;
using CRM.Services.Data.ViewModels.Contracts.Contracts;
using CRM.Services.Data.ViewModels.Contracts.Providers;
using CRM.Services.Data.ViewModels.Contracts.TypeOfCompanies;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
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
                .ProjectTo<TypeOfCompanyViewModel>()
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
                .ProjectTo<ProviderViewModel>()
                .FirstOrDefault(p => p.Id == providerId);

            return provider;
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

        public List<ProviderViewModel> ReadProviders(string searchboxProviders)
        {
            List<ProviderViewModel> providers;

            if (string.IsNullOrEmpty(searchboxProviders) || searchboxProviders == "")
            {
                providers = this.Data.Providers
                .All()
                .ProjectTo<ProviderViewModel>()
                .ToList();
            }
            else
            {
                providers = this.Data.Providers
                .All()
                .ProjectTo<ProviderViewModel>()
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
                TypeOfCompany = provider.TypeOfCompany,
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
                Comments = provider.Comments,
                ContractTemplate = provider.ContractTemplate,
                Channels = new List<Channel>(),
                Contracts = new List<ProviderContract>(),
                Discussions = new List<Discussion>()
            };

            if (string.IsNullOrEmpty(provider.LogoLink) && string.IsNullOrEmpty(newProvider.LogoLink))
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
            providerFromDb.TypeOfCompany = provider.TypeOfCompany;
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
            providerFromDb.Comments = provider.Comments;
            providerFromDb.ContractTemplate = provider.ContractTemplate;

            if (string.IsNullOrEmpty(provider.LogoLink) && string.IsNullOrEmpty(provider.LogoLink))
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
                .ProjectTo<ProviderViewModel>()
                .FirstOrDefault(p => p.Id == providerId)
                .Name;

            return providerName;
        }
    }
}
