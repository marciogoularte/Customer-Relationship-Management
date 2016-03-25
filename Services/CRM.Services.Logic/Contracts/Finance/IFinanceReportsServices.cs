namespace CRM.Services.Logic.Contracts.Finance
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.Reports;

    public interface IFinanceReportsServices : IService
    {
        List<SearchedItemDropDown> GetClients();

        List<SearchedItemDropDown> GetProviders();

        List<SearchedItemDropDown> GetMonths();

        List<SearchedItemDropDown> GetYears();
    }
}
