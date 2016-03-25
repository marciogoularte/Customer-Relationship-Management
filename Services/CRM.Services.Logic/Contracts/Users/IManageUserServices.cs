namespace CRM.Services.Logic.Contracts.Users
{
    using Base;
    using Data.ViewModels.Users.ManageUser;

    public interface IManageUserServices : IService
    {
        ManageUserViewModel GetUserData(string userId);

        void ManageData(string userId, ManageUserViewModel user);
    }
}
