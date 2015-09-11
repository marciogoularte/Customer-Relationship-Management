namespace TVChannelsCRM.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ITVChannelsCRMDbContext
    {
        IDbSet<Provider> Providers { get; set; }

        IDbSet<Client> Clients { get; set; }

        IDbSet<Activity> Activities { get; set; }

        IDbSet<SchedulerTask> SchedulerTasks { get; set; }

        IDbSet<Channel> Channels { get; set; }

        IDbSet<Contract> Contracts { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
