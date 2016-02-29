namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.Trds;

    public interface ITrdsServices : IService
    {
        List<string> GetTrdsData();

        List<TrdViewModel> ReadTrds(string searchbox, int clientId, bool showAll);

        TrdViewModel CreateTrd(TrdViewModel trd, string clientIdString);

        TrdViewModel TrdDetails(int trdId);

        TrdViewModel UpdateTrd(TrdViewModel trd);

        TrdViewModel DestroyTrd(TrdViewModel trd);
    }
}