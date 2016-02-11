namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Contracts;
    using Data.ViewModels.Contracts.Providers;

    public interface IContractsServices : IService
    {
        List<DatabaseDataDropdownViewModel> AllClientsContracts(int clientId);

        ClientContractViewModel ClientContractInformation(int contractId);

        List<ChannelViewModel> GetChannels(int contractId);

        ProviderContractViewModel ProviderContractDetails(int contractId);

        List<string> ClientsContractsNames();

        List<string> ProvidersContractsNames();

        List<ClientContractViewModel> ReadClientsContracts(string searchTerm, int clientId, bool showAll);

        List<ProviderContractViewModel> ReadProvidersContracts(string searchbox, int providerId, bool showAll);

        ClientContractViewModel CreateClientContract(ClientContractViewModel contract, int currentClientId);

        ProviderContractViewModel CreateProviderContract(ProviderContractViewModel contract, int currentProviderId);

        ClientContractViewModel UpdateClientContract(ClientContractViewModel contract);

        ProviderContractViewModel UpdateProviderContract(ProviderContractViewModel contract);

        ClientContractViewModel DestroyClientContract(ClientContractViewModel contract);

        ProviderContractViewModel DestroyProviderContract(ProviderContractViewModel contract);

        ClientContractChannelViewModel AllClientContractChannels(int clientContractId);

        void AddOrRemoveChannelFromClientContract(int clientContractId, int channelId);

        ContractResponseModel GenerateContractTemplate(int providerId, int contractId);
    }
}