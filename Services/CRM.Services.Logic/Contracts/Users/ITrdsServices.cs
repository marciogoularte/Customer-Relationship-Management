namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Users.Trds;

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