namespace TVChannelsCRM.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;


    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Contracts;
    using Migrations;
    using Contracts.CodeFirstConventions;

    public class TVChannelsCRMDbContext : IdentityDbContext<User>, ITVChannelsCRMDbContext
    {
        public TVChannelsCRMDbContext()
            : this("CRMConnection")
        {
        }

        public TVChannelsCRMDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TVChannelsCRMDbContext, Configuration>());
        }

        public virtual IDbSet<Provider> Providers { get; set; }

        public virtual IDbSet<Client> Clients { get; set; }

        public virtual IDbSet<Activity> Activities { get; set; }

        public virtual IDbSet<SchedulerTask> SchedulerTasks { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static TVChannelsCRMDbContext Create()
        {
            return new TVChannelsCRMDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
