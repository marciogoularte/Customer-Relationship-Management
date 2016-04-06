using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRM.Data
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Models.Finance;
    using Models.Marketing;
    using Contracts;
    using Repositories.Base;

    public class CRMData : ICRMData
    {
        private readonly ICRMDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public CRMData(ICRMDbContext context)
        {
            this.context = context;
        }

        public ICRMDbContext Context => this.context;

        public IRepository<User> Users => this.GetRepository<User>();

        public IDeletableEntityRepository<Provider> Providers => this.GetDeletableEntityRepository<Provider>();

        public IDeletableEntityRepository<Client> Clients => this.GetDeletableEntityRepository<Client>();

        public IDeletableEntityRepository<Activity> Activities => this.GetDeletableEntityRepository<Activity>();

        public IDeletableEntityRepository<SchedulerTask> SchedulerTasks => this.GetDeletableEntityRepository<SchedulerTask>();

        public IDeletableEntityRepository<Channel> Channels => this.GetDeletableEntityRepository<Channel>();

        public IDeletableEntityRepository<ClientContract> ClientContracts => this.GetDeletableEntityRepository<ClientContract>();

        public IDeletableEntityRepository<ProviderContract> ProviderContracts => this.GetDeletableEntityRepository<ProviderContract>();

        public IDeletableEntityRepository<Invoice> Invoices => this.GetDeletableEntityRepository<Invoice>();

        public IDeletableEntityRepository<Discussion> Discussions => this.GetDeletableEntityRepository<Discussion>();

        public IDeletableEntityRepository<Trd> Trds => this.GetDeletableEntityRepository<Trd>();

        public IDeletableEntityRepository<TypeOfCompany> TypeOfCompanies => this.GetDeletableEntityRepository<TypeOfCompany>();

        public IDeletableEntityRepository<Campaign> Campaigns => this.GetDeletableEntityRepository<Campaign>();

        public IDeletableEntityRepository<Media> Media => this.GetDeletableEntityRepository<Media>();

        public IDeletableEntityRepository<Operator> Operators => this.GetDeletableEntityRepository<Operator>();

        public IDeletableEntityRepository<Pr> Prs => this.GetDeletableEntityRepository<Pr>();

        public IDeletableEntityRepository<MarketingPartner> MarketingPartners => this.GetDeletableEntityRepository<MarketingPartner>();

        public IDeletableEntityRepository<SocialPartner> SocialPartners => this.GetDeletableEntityRepository<SocialPartner>();

        public IDeletableEntityRepository<FinanceInvoice> FinanceInvoices => this.GetDeletableEntityRepository<FinanceInvoice>();

        public IDeletableEntityRepository<Frz> Frzs => this.GetDeletableEntityRepository<Frz>();

        public IDeletableEntityRepository<Payment> Payments => this.GetDeletableEntityRepository<Payment>();

        //public IDbSet<IdentityUserRole> IdentityUserRoles => this.GetDbSet<IdentityUserRole>();

        public IRepository<IdentityRole> IdentityRoles => this.GetRepository<IdentityRole>();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            this.context?.Dispose();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (this.repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)this.repositories[typeof(T)];
            }

            var type = typeof(GenericRepository<T>);
            this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity, IEntity
        {
            if (this.repositories.ContainsKey(typeof(T)))
            {
                return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
            }

            var type = typeof(DeletableEntityRepository<T>);
            this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
