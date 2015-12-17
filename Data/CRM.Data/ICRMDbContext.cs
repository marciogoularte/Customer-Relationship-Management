namespace CRM.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;
    using Models.Finance;
    using Models.Marketing;

    public interface ICRMDbContext
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

        IDbSet<Media> Media { get; set; }

        IDbSet<Operator> Operators { get; set; }

        IDbSet<Pr> Prs { get; set; }

        IDbSet<MarketingPartner> MarketingPartners { get; set; }

        IDbSet<SocialPartner> SocialPartners { get; set; }

        IDbSet<FinanceInvoice> FinanceInvoices { get; set; }

        IDbSet<Frz> Frzs { get; set; }

        IDbSet<Payment> Payments { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
