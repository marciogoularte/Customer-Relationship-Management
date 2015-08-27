namespace TVChannelsCRM.Data
{
    using Models;
    using Contracts;

    public interface ITVChannelsCRMData
    {
        ITVChannelsCRMDbContext Context { get; }

        IDeletableEntityRepository<Provider> Providers { get; }

        IDeletableEntityRepository<Client> Clients { get; }

        IDeletableEntityRepository<Activity> Activities { get; }

        IDeletableEntityRepository<SchedulerTask> SchedulerTasks { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
