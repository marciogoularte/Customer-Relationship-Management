namespace CRM.Services.Logic.Contracts.Users
{
    using System.Linq;

    using Base;
    using Data.ViewModels.Users.Profile;
    using Data.ViewModels.Administration.Admin;

    public interface IProfileServices : IService
    {
        UserViewModel Index(string userId, string loggedUserId);

        IQueryable<SchedulerTaskViewModel> Read(string userId);

        SchedulerTaskViewModel Destroy(SchedulerTaskViewModel task);

        SchedulerTaskViewModel Create(SchedulerTaskViewModel task);

        SchedulerTaskViewModel Update(SchedulerTaskViewModel task);
    }
}