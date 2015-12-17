namespace CRM.Services.Logic.Contracts.Finance
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.Frz;

    public interface IFrzsServices : IService
    {
        List<string> Index();

        FrzViewModel FrzInformation(int frzId);

        FrzViewModel FrzDetails(int frzId);

        List<FrzViewModel> ReadFrzs(string searchboxFrz);

        FrzViewModel CreateFrz(FrzViewModel givenFrz);

        FrzViewModel UpdateFrz(FrzViewModel givenFrz);

        FrzViewModel DestroyFrz(FrzViewModel givenFrz);

        string GetFrzById(int frzId);
    }
}
