﻿namespace CRM.Services.Logic.Services.Contractors
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Contractors;
    using Data.ViewModels.Contracts.Trds;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.Invoices;
    using Data.ViewModels.Contracts.Providers;
    using Data.ViewModels.Contracts.TypeOfCompanies;

    public class ContractsServices : IContractsServices
    {
        private ICRMData Data { get; set; }

        public ContractsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<DatabaseDataDropdownViewModel> AllClientsContracts(int clientId)
        {
            var providers = this.Data.Providers
                .All()
                .ProjectTo<ProviderViewModel>()
                .ToList();

            var vm = providers
                .Select(provider => new DatabaseDataDropdownViewModel
                {
                    Text = provider.Name,
                    Value = provider.Id
                })
                .ToList();

            return vm;
        }

        public ClientContractViewModel ClientContractInformation(int contractId)
        {
            var contract = this.Data.ClientContracts
                .All()
                .ProjectTo<ClientContractViewModel>()
                .FirstOrDefault(p => p.Id == contractId);

            if (contract == null || contract.ProviderId == null)
            {
                return contract;
            }

            return contract;
        }

        public List<ChannelViewModel> GetChannels(int contractId)
        {
            var contract = this.Data.ClientContracts
                .All()
                .FirstOrDefault(c => c.Id == contractId);

            var channels = new List<ChannelViewModel>();

            if (contract == null || contract.Provider == null || contract.Provider.Channels.Count == 0)
            {
                return channels;
            }

            foreach (var channel in contract.Provider.Channels)
            {
                var channelAsModel = this.Data.Channels
                    .All()
                    .Where(c => c.Id == channel.Id)
                    .ProjectTo<ChannelViewModel>()
                    .FirstOrDefault();

                channels.Add(channelAsModel);
            }

            return channels;
        }

        public ProviderContractViewModel ProviderContractDetails(int contractId)
        {
            var contract = this.Data.ProviderContracts
                .All()
                .ProjectTo<ProviderContractViewModel>()
                .FirstOrDefault(c => c.Id == contractId);

            return contract;
        }

        public List<string> ClientsContractsNames()
        {
            var contractsNames = this.Data.ClientContracts
                .All()
                .Select(c => c.TypeOfContract)
                .ToList();

            return contractsNames;
        }

        public List<string> ProvidersContractsNames()
        {
            var contractsNames = this.Data.ProviderContracts
                .All()
                .Select(c => c.TypeOfContract)
                .ToList();

            return contractsNames;
        }

        public List<ClientContractViewModel> ReadClientsContracts(string searchTerm, int clientId, bool showAll)
        {
            List<ClientContractViewModel> contracts;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                contracts = this.Data.ClientContracts
                .All()
                .Where(c => c.TypeOfContract.Contains(searchTerm) && c.ClientId == clientId)
                .ProjectTo<ClientContractViewModel>()
                .ToList();
            }
            else
            {
                if (showAll == false)
                {
                    contracts = this.Data.ClientContracts
                    .All()
                    .Where(c => c.ClientId == clientId && c.IsVisible)
                    .ProjectTo<ClientContractViewModel>()
                    .ToList();
                }
                else
                {
                    contracts = this.Data.ClientContracts
                    .All()
                    .Where(c => c.ClientId == clientId)
                    .ProjectTo<ClientContractViewModel>()
                    .ToList();
                }
            }

            return contracts;
        }

        public List<ProviderContractViewModel> ReadProvidersContracts(string searchbox, int providerId, bool showAll)
        {
            List<ProviderContractViewModel> contracts;

            if (!string.IsNullOrEmpty(searchbox))
            {
                contracts = this.Data.ProviderContracts
                .All()
                .Where(c => c.TypeOfContract.Contains(searchbox) && c.ProviderId == providerId)
                .ProjectTo<ProviderContractViewModel>()
                .ToList();
            }
            else
            {
                if (showAll == false)
                {
                    contracts = this.Data.ProviderContracts
                    .All()
                    .Where(c => c.ProviderId == providerId && c.IsVisible)
                    .ProjectTo<ProviderContractViewModel>()
                    .ToList();
                }
                else
                {
                    contracts = this.Data.ProviderContracts
                    .All()
                    .Where(c => c.ProviderId == providerId)
                    .ProjectTo<ProviderContractViewModel>()
                    .ToList();
                }
            }

            return contracts;
        }

        public ClientContractViewModel CreateClientContract(ClientContractViewModel contract, int currentClientId)
        {
            int clientType;
            string type;

            try
            {
                clientType = int.Parse(this.Data.Clients
                    .GetById(currentClientId)
                    .TypeOfCompany);

                type = this.Data.TypeOfCompanies
                        .GetById(clientType)
                        .Type;
            }
            catch (Exception)
            {
                type = this.Data.Clients
                    .GetById(currentClientId)
                    .TypeOfCompany;
            }

            var newContract = new ClientContract
            {
                StartDate = contract.StartDate,
                TypeOfContract = type,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                Tier = contract.Tier,
                ClientId = currentClientId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments,
                Channels = new List<Channel>(),
                Frequency = contract.Frequency,
                MonthlyFee = contract.MonthlyFee,
                AnnualIndexation = contract.AnnualIndexation,
                IsVisible = contract.IsVisible
            };

            if (contract.ProviderId != null)
            {
                newContract.ProviderId = int.Parse(contract.ProviderId);
            }

            this.Data.ClientContracts.Add(newContract);
            this.Data.SaveChanges();

            contract.Id = newContract.Id;

            return contract;
        }

        public ProviderContractViewModel CreateProviderContract(ProviderContractViewModel contract, int currentProviderId)
        {
            var providerType = int.Parse(this.Data.Providers
                .GetById(currentProviderId)
                .TypeOfCompany);

            var type = this.Data.TypeOfCompanies
                .GetById(providerType)
                .Type;

            var newContract = new ProviderContract
            {
                StartDate = contract.StartDate,
                TypeOfContract = type,
                EndDate = contract.EndDate,
                NoticePeriod = contract.NoticePeriod,
                BillingStartDate = contract.BillingStartDate,
                BillingEndDate = contract.BillingEndDate,
                NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate,
                AcceptingReports = contract.AcceptingReports,
                GoverningLaw = contract.GoverningLaw,
                ProviderId = currentProviderId,
                CreatedOn = DateTime.Now,
                Comments = contract.Comments,
                AnnualIndexation = contract.AnnualIndexation,
                IsVisible = contract.IsVisible
            };

            this.Data.ProviderContracts.Add(newContract);
            this.Data.SaveChanges();

            contract.Id = newContract.Id;

            return contract;
        }

        public ClientContractViewModel UpdateClientContract(ClientContractViewModel contract)
        {
            var contractFromDb = this.Data.ClientContracts
                .All()
                .FirstOrDefault(c => c.Id == contract.Id);

            contractFromDb.StartDate = contract.StartDate;
            contractFromDb.TypeOfContract = contract.TypeOfContract;
            contractFromDb.EndDate = contract.EndDate;
            contractFromDb.NoticePeriod = contract.NoticePeriod;
            contractFromDb.BillingStartDate = contract.BillingStartDate;
            contractFromDb.BillingEndDate = contract.BillingEndDate;
            contractFromDb.NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate;
            contractFromDb.AcceptingReports = contract.AcceptingReports;
            contractFromDb.GoverningLaw = contract.GoverningLaw;
            contractFromDb.Tier = contract.Tier;
            contractFromDb.CreatedOn = DateTime.Now;
            contractFromDb.Comments = contract.Comments;
            contractFromDb.ProviderId = int.Parse(contract.ProviderId);
            contractFromDb.Frequency = contract.Frequency;
            contractFromDb.MonthlyFee = contract.MonthlyFee;
            contractFromDb.IsVisible = contract.IsVisible;
            contractFromDb.AnnualIndexation = contract.AnnualIndexation;

            this.Data.SaveChanges();

            return contract;
        }

        public ProviderContractViewModel UpdateProviderContract(ProviderContractViewModel contract)
        {
            var contractFromDb = this.Data.ProviderContracts
                .All()
                .FirstOrDefault(c => c.Id == contract.Id);

            contractFromDb.StartDate = contract.StartDate;
            contractFromDb.TypeOfContract = contract.TypeOfContract;
            contractFromDb.EndDate = contract.EndDate;
            contractFromDb.NoticePeriod = contract.NoticePeriod;
            contractFromDb.BillingStartDate = contract.BillingStartDate;
            contractFromDb.BillingEndDate = contract.BillingEndDate;
            contractFromDb.NumberOfDaysForPaymentDueDate = contract.NumberOfDaysForPaymentDueDate;
            contractFromDb.AcceptingReports = contract.AcceptingReports;
            contractFromDb.GoverningLaw = contract.GoverningLaw;
            contractFromDb.CreatedOn = DateTime.Now;
            contractFromDb.Comments = contract.Comments;
            contractFromDb.IsVisible = contract.IsVisible;
            contractFromDb.AnnualIndexation = contract.AnnualIndexation;

            this.Data.SaveChanges();

            return contract;
        }

        public ClientContractViewModel DestroyClientContract(ClientContractViewModel contract)
        {
            this.Data.ClientContracts.Delete(contract.Id);
            this.Data.SaveChanges();

            return contract;
        }

        public ProviderContractViewModel DestroyProviderContract(ProviderContractViewModel contract)
        {
            this.Data.ProviderContracts.Delete(contract.Id);
            this.Data.SaveChanges();

            return contract;
        }

        public ClientContractChannelViewModel AllClientContractChannels(int clientContractId)
        {
            var result = new ClientContractChannelViewModel();
            try
            {
                result = this.GetClientContractChannels(clientContractId);
            }
            catch (Exception ex)
            {
                try
                {
                    result = this.GetClientContractChannels(clientContractId);
                }
                catch (Exception ex1)
                {
                    try
                    {
                        result = this.GetClientContractChannels(clientContractId);
                    }
                    catch (Exception ex2)
                    {
                        return new ClientContractChannelViewModel();
                    }
                }
            }

            return result;
        }

        public void AddOrRemoveChannelFromClientContract(int clientContractId, int channelId)
        {
            var contract = this.Data.ClientContracts
                .GetById(clientContractId);
            var containsIt = false;

            foreach (var contractChannel in contract.Channels.Where(contractChannel => contractChannel.Id == channelId))
            {
                containsIt = true;
            }

            var channel = this.Data.Channels
                .All()
                .FirstOrDefault(c => c.Id == channelId);

            if (containsIt)
            {
                contract.Channels.Remove(channel);
            }
            else
            {
                contract.Channels.Add(channel);
            }

            this.Data.SaveChanges();
            this.Data.Channels.SaveChanges();
            this.Data.ClientContracts.SaveChanges();
        }

        public ContractResponseModel GenerateContractTemplate(int providerId, int contractId)
        {
            var contract = this.Data.ClientContracts
                .All()
                .ProjectTo<ClientContractViewModel>()
                .FirstOrDefault(c => c.Id == contractId);

            var contractTemplate = this.Data.Providers
                .GetById(providerId).ContractTemplate;

            var client = this.Data.Clients
                .All()
                .ProjectTo<ClientViewModel>()
                .FirstOrDefault(c => c.Id == contract.ClientId);

            var provider = this.Data.Providers
                .All()
                .ProjectTo<ProviderViewModel>()
                .FirstOrDefault(p => p.Id == providerId);

            var invoices = this.Data.Invoices
                .All()
                .ProjectTo<InvoiceViewModel>()
                .Where(i => i.ClientContractId == contract.Id)
                .ToList();

            var channelsIds = this.Data.ClientContracts.GetById(contractId)
                .Channels
                .Select(c => c.Id)
                .ToList();

            var channels = this.Data.Channels
                .All()
                .Where(c => channelsIds.Contains(c.Id))
                .ProjectTo<ChannelViewModel>()
                .ToList();

            var trds = this.Data.Trds
                .All()
                .ProjectTo<TrdViewModel>()
                .Where(t => t.ClientId == contract.ClientId)
                .ToList();

            var clientTypeIf = int.Parse(client.TypeOfCompany);

            var typeOfClientContract = this.Data.TypeOfCompanies
                .All()
                .ProjectTo<TypeOfCompanyViewModel>()
                .FirstOrDefault(t => t.Id == clientTypeIf);

            var totalMgSubs = 0.0;
            var totalCps = 0.0;
            foreach (var invoice in invoices)
            {
                var mgSubsAsInt = invoice.MgSubs;
                var cpsInt = invoice.MgSubs;

                totalMgSubs += mgSubsAsInt;
                totalCps += cpsInt;
            }

            var sum = 0.0;
            if (invoices.Any())
            {
                foreach (var invoice in invoices)
                {
                    var msSubsAsDouble = invoice.MgSubs;
                    var cpsAsDouble = invoice.Cps;
                    sum += (msSubsAsDouble * cpsAsDouble);
                }
            }

            var response = new ContractResponseModel()
            {
                Sum = sum,
                Client = client,
                Provider = provider,
                Channels = channels,
                Invoices = invoices,
                Trds = trds,
                MgSubs = totalMgSubs,
                Cps = totalCps,
                ClientContractType = typeOfClientContract,
                Contract = contract,
                ContractTemplate = contractTemplate
            };

            return response;
        }

        private ClientContractChannelViewModel GetClientContractChannels(int clientContractId)
        {
            var clientContractProviderId = this.Data.ClientContracts
              .All()
              .Where(c => c.Id == clientContractId)
              .FirstOrDefault(c => c.Id == clientContractId)
              .ProviderId;

            var providerChannels = this.Data.Channels
                .All()
                .ProjectTo<ChannelViewModel>()
                .Where(c => c.ProviderId == clientContractProviderId)
                .ToList();

            var clientContract = this.Data.ClientContracts
                .GetById(clientContractId);

            var clientContractChannels = new List<ChannelViewModel>();
            foreach (var channel in clientContract.Channels)
            {
                var channelAsModel = this.Data.Channels
                    .All()
                    .Where(c => c.Id == channel.Id)
                    .ProjectTo<ChannelViewModel>()
                    .FirstOrDefault();

                clientContractChannels.Add(channelAsModel);
            }

            var indexesOfElementForRemove = new List<int>();
            foreach (var channel in clientContractChannels)
            {
                indexesOfElementForRemove.AddRange(
                    from providerChannel in providerChannels
                    where providerChannel.Id == channel.Id
                    select providerChannels
                        .FindIndex(p => p.Id == channel.Id));
            }
            var sortedListFromIndexes = indexesOfElementForRemove.OrderByDescending(i => i).ToList();

            foreach (var index in sortedListFromIndexes)
            {
                providerChannels.RemoveAt(index);
            }

            var result = new ClientContractChannelViewModel()
            {
                ProviderChannels = providerChannels,
                ClientContractChannels = clientContractChannels,
                ClientContractId = clientContractId
            };

            return result;
        }
    }
}