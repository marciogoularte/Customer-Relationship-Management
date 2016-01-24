namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Users.UserActivities;

    public interface IUserActivitiesServices : IService
    {
        List<UserActivity> GetPreviousActivities(string userId);

        List<UserActivity> GetUpcomingActivities(string userId);

        void FinishActivity(int activityId, string comments, string date);
    }
}
