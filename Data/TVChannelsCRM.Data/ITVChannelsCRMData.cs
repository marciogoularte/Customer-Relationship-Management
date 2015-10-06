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

        IDeletableEntityRepository<Channel> Channels { get; }

        IDeletableEntityRepository<ClientContract> ClientContracts { get; }

        IDeletableEntityRepository<ProviderContract> ProviderContracts { get; }

        IDeletableEntityRepository<Invoice> Invoices { get; }

        IDeletableEntityRepository<Discussion> Discussions { get; }

<<<<<<< HEAD
        IDeletableEntityRepository<Trd> Trds { get; }

        IDeletableEntityRepository<TypeOfCompany> TypeOfCompanies { get; }

        //IDeletableEntityRepository<ContractTemplate> ContractTemplates { get; }

=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
