namespace CRM.Services.Logic.Contracts.Finance
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.Reports;

    public interface IReportsServices : IService
    {
        List<SearchedItemDropDown> GetClients();

        List<SearchedItemDropDown> GetProviders();

        List<SearchedItemDropDown> GetMonths();

        List<SearchedItemDropDown> GetYears();
    }
}
