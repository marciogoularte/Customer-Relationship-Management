namespace CRM.Services.Logic.Contracts.Administration
{
    using System.Collections.Generic;
    
    using Base;
    using Data.ViewModels.Users.Activities;

    public interface IActivitiesServices : IService
    {
        List<ActivityViewModel> GetActivitiesInAdminRole();

        List<ActivityViewModel> GetActivitiesInNormalRole();
    }
}