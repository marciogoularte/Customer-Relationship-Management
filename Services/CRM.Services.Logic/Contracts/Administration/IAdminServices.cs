namespace CRM.Services.Logic.Contracts.Administration
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Administration.Admin;

    public interface IAdminServices : IService
    {
        UserViewModel CreateUser(UserViewModel user);

        UserViewModel DestroyUser(UserViewModel user);

        List<string> Index(string loggedUserId);

        List<UserViewModel> ReadUsers(string searchboxUsers, string loggedUserId);

        UserViewModel UpdateUser(UserViewModel user);
    }
}