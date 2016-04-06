namespace CRM.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Contracts;
    using Models;
    using Models.Finance;
    using Models.Marketing;

    public interface ICRMData
    {
        ICRMDbContext Context { get; }

        IDeletableEntityRepository<Provider> Providers { get; }

        IDeletableEntityRepository<Client> Clients { get; }

        IDeletableEntityRepository<Activity> Activities { get; }

        IDeletableEntityRepository<SchedulerTask> SchedulerTasks { get; }

        IDeletableEntityRepository<Channel> Channels { get; }

        IDeletableEntityRepository<ClientContract> ClientContracts { get; }

        IDeletableEntityRepository<ProviderContract> ProviderContracts { get; }

        IDeletableEntityRepository<Invoice> Invoices { get; }

        IDeletableEntityRepository<Discussion> Discussions { get; }

        IDeletableEntityRepository<Trd> Trds { get; }

        IDeletableEntityRepository<TypeOfCompany> TypeOfCompanies { get; }

        IDeletableEntityRepository<Campaign> Campaigns { get; }

        IDeletableEntityRepository<Media> Media { get; }

        IDeletableEntityRepository<Operator> Operators { get; }

        IDeletableEntityRepository<Pr> Prs { get; }

        IDeletableEntityRepository<MarketingPartner> MarketingPartners { get; }

        IDeletableEntityRepository<SocialPartner> SocialPartners { get; }

        IDeletableEntityRepository<FinanceInvoice> FinanceInvoices { get; }

        IDeletableEntityRepository<Frz> Frzs { get; }

        IDeletableEntityRepository<Payment> Payments { get; }

        //IDbSet<IdentityUserRole> IdentityUserRoles { get; }

        IRepository<IdentityRole> IdentityRoles { get; }

        //IDeletableEntityRepository<ContractTemplate> ContractTemplates { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
