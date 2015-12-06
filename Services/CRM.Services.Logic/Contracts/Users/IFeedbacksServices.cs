namespace CRM.Services.Logic.Contracts.Users
{
    using Base;
    using Data.ViewModels.Users.Feedbacks;

    public interface IFeedbacksServices : IService
    {
        void SendEmail(string to, FeedbackViewModel model);
    }
}