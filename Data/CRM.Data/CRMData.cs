namespace CRM.Data
{
    using System;
    using System.Collections.Generic;

    using Models;
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

        public ICRMDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        //public IRepository<T> GetGenericRepository<T>() where T : class
        //{
        //    if (typeof(T).IsAssignableFrom(typeof(DeletableEntity)))
        //    {
        //        return this.GetDeletableEntityRepository<T>();
        //    }

        //    return this.GetRepository<T>();
        //}

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        //  public IRepository<IdentityUserRole> UserRoles
        //  {
        //      get { return this.GetRepository<IdentityUserRole>()}
        //  }

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

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity, IEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }

        public IDeletableEntityRepository<Provider> Providers
        {
            get { return this.GetDeletableEntityRepository<Provider>(); }
        }

        public IDeletableEntityRepository<Client> Clients
        {
            get { return this.GetDeletableEntityRepository<Client>(); }
        }

        public IDeletableEntityRepository<Activity> Activities
        {
            get { return this.GetDeletableEntityRepository<Activity>(); }
        }

        public IDeletableEntityRepository<SchedulerTask> SchedulerTasks
        {
            get { return this.GetDeletableEntityRepository<SchedulerTask>(); }
        }

        public IDeletableEntityRepository<Channel> Channels
        {
            get { return this.GetDeletableEntityRepository<Channel>(); }
        }

        public IDeletableEntityRepository<ClientContract> ClientContracts
        {
            get { return this.GetDeletableEntityRepository<ClientContract>(); }
        }

        public IDeletableEntityRepository<ProviderContract> ProviderContracts
        {
            get { return this.GetDeletableEntityRepository<ProviderContract>(); }
        }

        public IDeletableEntityRepository<Invoice> Invoices
        {
            get { return this.GetDeletableEntityRepository<Invoice>(); }
        }

        public IDeletableEntityRepository<Discussion> Discussions
        {
            get { return this.GetDeletableEntityRepository<Discussion>(); }
        }

        public IDeletableEntityRepository<Trd> Trds
        {
            get { return this.GetDeletableEntityRepository<Trd>(); }
        }

        public IDeletableEntityRepository<TypeOfCompany> TypeOfCompanies
        {
            get { return this.GetDeletableEntityRepository<TypeOfCompany>(); }
        }

        public IDeletableEntityRepository<Campaign> Campaigns
        {
            get { return this.GetDeletableEntityRepository<Campaign>(); }
        }
    }
}
