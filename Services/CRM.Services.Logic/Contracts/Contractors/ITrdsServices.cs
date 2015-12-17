using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.Trds;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface ITrdsServices : IService
    {
        List<string> GetTrdsData();

        List<TrdViewModel> ReadTrds(string searchbox, int clientId);

        TrdViewModel CreateTrd(TrdViewModel trd, string clientIdString);

        TrdViewModel TrdDetails(int trdId);

        TrdViewModel UpdateTrd(TrdViewModel trd);

        TrdViewModel DestroyTrd(TrdViewModel trd);
    }
}