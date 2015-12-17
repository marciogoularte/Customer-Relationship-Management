using CRM.Services.Data.ViewModels.Contracts.Activities;

namespace CRM.Services.Logic.Contracts.Administration
{
    using System.Collections.Generic;
    
    using Base;

    public interface IActivitiesServices : IService
    {
        List<ActivityViewModel> GetActivitiesInAdminRole();

        List<ActivityViewModel> GetActivitiesInNormalRole();
    }
}