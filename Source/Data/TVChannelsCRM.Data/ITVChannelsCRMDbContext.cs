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

        IDbSet<ClientContract> ClientContracts { get; set; }

        IDbSet<ProviderContract> ProviderContracts { get; set; }

        IDbSet<Invoice> Invoices { get; set; }

        IDbSet<Discussion> Discussions { get; set; }

        IDbSet<Trd> Trds { get; set; }

        IDbSet<TypeOfCompany> TypeOfCompanies { get; set; }

        IDbSet<Campaign> Campaigns { get; set; }

        //IDbSet<ContractTemplate> ContractTemplates { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
