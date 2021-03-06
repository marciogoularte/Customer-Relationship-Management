﻿namespace CRM.Services.Logic.Services.Administration
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using Contracts.Administration;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.Discussions;
    using Data.ViewModels.Contracts.Invoices;
    using Data.ViewModels.Contracts.Providers;
    
    public class DeletedItemsServices : IDeletedItemsServices
    {
        private ICRMData Data { get; set; }

        public DeletedItemsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> DeletedClients()
        {
            var clientsNames = this.Data.Clients
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.Name)
                .ToList();

            return clientsNames;
        }

        public List<string> DeletedProviders()
        {
            var providersNames = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.IsDeleted == true)
                .Select(p => p.Name)
                .ToList();

            return providersNames;
        }

        public List<string> DeletedChannels()
        {
            var channelsNames = this.Data.Channels
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.Name)
                .ToList();

            return channelsNames;
        }

        public List<string> DeletedClientContracts()
        {
            var contractsNames = this.Data.ClientContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.TypeOfContract.ToString())
                .ToList();

            return contractsNames;
        }

        public List<string> DeletedProviderContracts()
        {
            var contractsNames = this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.TypeOfContract.ToString())
                .ToList();

            return contractsNames;
        }

        public List<string> DeletedDiscussions()
        {
            var discussionsSubjects = this.Data.Discussions
                .AllWithDeleted()
                .Where(d => d.IsDeleted == true)
                .Select(d => d.SubjectOfDiscussion)
                .ToList();

            return discussionsSubjects;
        }

        public List<string> DeletedInvoices()
        {
            var mgSubs = this.Data.Invoices
                .AllWithDeleted()
                .Where(i => i.IsDeleted)
                .Select(i => i.MgSubs.ToString())
                .ToList();

            return mgSubs;
        }

        public List<ClientViewModel> ReadDeletedClients(string searchboxDeletedClients)
        {
            List<ClientViewModel> clients;

            if (string.IsNullOrEmpty(searchboxDeletedClients) || searchboxDeletedClients == "")
            {
                clients = this.Data.Clients
                 .AllWithDeleted()
                 .Where(c => c.IsDeleted == true)
                 .ProjectTo<ClientViewModel>()
                 .ToList();
            }
            else
            {
                clients = this.Data.Clients
                 .AllWithDeleted()
                 .Where(c => c.Name.Contains(searchboxDeletedClients) && c.IsDeleted == true)
                 .ProjectTo<ClientViewModel>()
                 .ToList();
            }

            return clients;
        }

        public List<ProviderViewModel> ReadDeletedProviders(string searchboxDeletedProviders)
        {
            List<ProviderViewModel> providers;

            if (string.IsNullOrEmpty(searchboxDeletedProviders) || searchboxDeletedProviders == "")
            {
                providers = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.IsDeleted == true)
                .ProjectTo<ProviderViewModel>()
                .ToList();
            }
            else
            {
                providers = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.Name.Contains(searchboxDeletedProviders) && p.IsDeleted == true)
                .ProjectTo<ProviderViewModel>()
                .ToList();
            }

            return providers;
        }

        public List<ChannelViewModel> ReadDeletedChannels(string searchboxDeletedChannels)
        {
            List<ChannelViewModel> channels;

            if (string.IsNullOrEmpty(searchboxDeletedChannels) || searchboxDeletedChannels == "")
            {
                channels = this.Data.Channels
                    .AllWithDeleted()
                    .Where(c => c.IsDeleted == true)
                    .ProjectTo<ChannelViewModel>()
                    .ToList();
            }
            else
            {
                channels = this.Data.Channels
                   .AllWithDeleted()
                   .Where(c => c.Name.Contains(searchboxDeletedChannels) && c.IsDeleted == true)
                   .ProjectTo<ChannelViewModel>()
                   .ToList();
            }

            return channels;
        }

        public List<ClientContractViewModel> ReadDeletedClientContracts(string searchboxDeletedContracts)
        {
            List<ClientContractViewModel> contracts;

            if (string.IsNullOrEmpty(searchboxDeletedContracts) || searchboxDeletedContracts == "")
            {
                contracts = this.Data.ClientContracts
                   .AllWithDeleted()
                   .Where(c => c.IsDeleted == true)
                   .ProjectTo<ClientContractViewModel>()
                   .ToList();
            }
            else
            {
                contracts = this.Data.ClientContracts
                .AllWithDeleted()
                .Where(c => c.TypeOfContract.ToString().Contains(searchboxDeletedContracts) && c.IsDeleted == true)
                .ProjectTo<ClientContractViewModel>()
                .ToList();
            }

            return contracts;
        }

        public List<ProviderContractViewModel> ReadDeletedProviderContracts(string searchboxDeletedContracts)
        {
            List<ProviderContractViewModel> contracts;

            if (string.IsNullOrEmpty(searchboxDeletedContracts) || searchboxDeletedContracts == "")
            {
                contracts = this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .ProjectTo<ProviderContractViewModel>()
                .ToList();
            }
            else
            {
                contracts = this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.TypeOfContract.ToString().Contains(searchboxDeletedContracts) && c.IsDeleted == true)
                .ProjectTo<ProviderContractViewModel>()
                .ToList();
            }

            return contracts;
        }

        public List<DiscussionViewModel> ReadDeletedDiscussions(string searchboxDeletedDiscussions)
        {
            List<DiscussionViewModel> discussions;

            if (string.IsNullOrEmpty(searchboxDeletedDiscussions) || searchboxDeletedDiscussions == "")
            {
                discussions = this.Data.Discussions
                .AllWithDeleted()
                .Where(d => d.IsDeleted == true)
                .ProjectTo<DiscussionViewModel>()
                .ToList();
            }
            else
            {
                discussions = this.Data.Discussions
                .AllWithDeleted()
                .Where(d => d.SubjectOfDiscussion.ToString().Contains(searchboxDeletedDiscussions) && d.IsDeleted == true)
                .ProjectTo<DiscussionViewModel>()
                .ToList();
            }

            return discussions;
        }

        public List<InvoiceViewModel> ReadDeletedInvoices(string searchboxDeletedInvoices)
        {
            List<InvoiceViewModel> invoices;

            if (string.IsNullOrEmpty(searchboxDeletedInvoices) || searchboxDeletedInvoices == "")
            {
                invoices = this.Data.Invoices
                .AllWithDeleted()
                .Where(i => i.IsDeleted == true)
                .ProjectTo<InvoiceViewModel>()
                .ToList();
            }
            else
            {
                invoices = this.Data.Invoices
                .AllWithDeleted()
                .Where(i => i.MgSubs.ToString().Contains(searchboxDeletedInvoices) && i.IsDeleted == true)
                .ProjectTo<InvoiceViewModel>()
                .ToList();
            }

            return invoices;
        }

        public void ConfirmRestoreClient(int clientId)
        {
            var client = this.Data.Clients
                .AllWithDeleted()
                .FirstOrDefault(c => c.Id == clientId);

            client.IsDeleted = false;
            client.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreProvider(int providerId)
        {
            var provider = this.Data.Providers
                .AllWithDeleted()
                .FirstOrDefault(p => p.Id == providerId);

            provider.IsDeleted = false;
            provider.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreChannel(int channelId)
        {
            var channel = this.Data.Channels
                .AllWithDeleted()
                .FirstOrDefault(p => p.Id == channelId);

            channel.IsDeleted = false;
            channel.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreClientContract(int contractId)
        {
            var contract = this.Data.ClientContracts
                .AllWithDeleted()
                .FirstOrDefault(p => p.Id == contractId);

            contract.IsDeleted = false;
            contract.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreProviderContract(int contractId)
        {
            var contract = this.Data.ProviderContracts
                .AllWithDeleted()
                .FirstOrDefault(p => p.Id == contractId);

            contract.IsDeleted = false;
            contract.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreDiscussion(int discussionId)
        {
            var discussion = this.Data.Discussions
                .AllWithDeleted()
                .FirstOrDefault(d => d.Id == discussionId);

            discussion.IsDeleted = false;
            discussion.DeletedOn = null;
            this.Data.SaveChanges();
        }

        public void ConfirmRestoreInvoice(int invoiceId)
        {
            var invoice = this.Data.Invoices
                .AllWithDeleted()
                .FirstOrDefault(p => p.Id == invoiceId);

            invoice.IsDeleted = false;
            invoice.DeletedOn = null;
            this.Data.SaveChanges();
        }
    }
}