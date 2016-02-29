namespace CRM.Services.Logic.Contracts.Administration
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Invoices;
    using Data.ViewModels.Contracts.Providers;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.Discussions;

    public interface IDeletedItemsServices : IService
    {
        void ConfirmRestoreChannel(int channelId);

        void ConfirmRestoreClient(int clientId);

        void ConfirmRestoreClientContract(int contractId);

        void ConfirmRestoreDiscussion(int discussionId);

        void ConfirmRestoreInvoice(int invoiceId);

        void ConfirmRestoreProvider(int providerId);

        void ConfirmRestoreProviderContract(int contractId);

        List<string> DeletedChannels();

        List<string> DeletedClientContracts();

        List<string> DeletedClients();

        List<string> DeletedDiscussions();

        List<string> DeletedInvoices();

        List<string> DeletedProviderContracts();

        List<string> DeletedProviders();

        List<ChannelViewModel> ReadDeletedChannels(string searchboxDeletedChannels);

        List<ClientContractViewModel> ReadDeletedClientContracts(string searchboxDeletedContracts);

        List<ClientViewModel> ReadDeletedClients(string searchboxDeletedClients);

        List<DiscussionViewModel> ReadDeletedDiscussions(string searchboxDeletedDiscussions);

        List<InvoiceViewModel> ReadDeletedInvoices(string searchboxDeletedInvoices);

        List<ProviderContractViewModel> ReadDeletedProviderContracts(string searchboxDeletedContracts);

        List<ProviderViewModel> ReadDeletedProviders(string searchboxDeletedProviders);
    }
}