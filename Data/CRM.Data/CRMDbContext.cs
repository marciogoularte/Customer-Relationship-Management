namespace CRM.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Models.Finance;
    using Models.Marketing;
    using Contracts;
    using Contracts.CodeFirstConventions;

    public class CRMDbContext : IdentityDbContext<User>, ICRMDbContext
    {
        public CRMDbContext()
            : this("CRMConnection")
        {
        }

        public CRMDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual IDbSet<Provider> Providers { get; set; }

        public virtual IDbSet<Client> Clients { get; set; }

        public virtual IDbSet<Activity> Activities { get; set; }

        public virtual IDbSet<SchedulerTask> SchedulerTasks { get; set; }

        public virtual IDbSet<Channel> Channels { get; set; }

        public virtual IDbSet<ClientContract> ClientContracts { get; set; }

        public virtual IDbSet<ProviderContract> ProviderContracts { get; set; }

        public virtual IDbSet<Invoice> Invoices { get; set; }

        public virtual IDbSet<Discussion> Discussions { get; set; }

        public virtual IDbSet<Trd> Trds { get; set; }

        public virtual IDbSet<TypeOfCompany> TypeOfCompanies { get; set; }

        public virtual IDbSet<Campaign> Campaigns { get; set; }

        public virtual IDbSet<Media> Media { get; set; }

        public virtual IDbSet<Operator> Operators { get; set; }

        public virtual IDbSet<Pr> Prs { get; set; }

        public virtual IDbSet<MarketingPartner> MarketingPartners { get; set; }

        public virtual IDbSet<SocialPartner> SocialPartners { get; set; }

        public virtual IDbSet<FinanceInvoice> FinanceInvoices { get; set; }

        public virtual IDbSet<Frz> Frzs { get; set; }

        public virtual IDbSet<Payment> Payments { get; set; }

        //public virtual IDbSet<IdentityUserRole> IdentityUserRoles { get; set; }

        //public virtual IDbSet<IdentityRole> IdentityRoles { get; set; }

        public DbContext DbContext => this;

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

        public static CRMDbContext Create()
        {
            return new CRMDbContext();
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
