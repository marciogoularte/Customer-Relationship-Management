namespace CRM.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Data;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<CRMDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CRMDbContext context)
        {
            if (context.Users.Any() || context.Clients.Any() || context.Providers.Any())
            {
                return;
            }

            this.AddMarketingUserProfiles(context);
            this.AddDealerUserProfiles(context);
            this.AddFinancialUserProfiles(context);
            this.AddAdministratorUserProfiles(context);
        }

        private void AddMarketingUserProfiles(CRMDbContext context)
        {
            // Create Dealer user role
            const string roleName = "Marketing";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);
            context.SaveChanges();
        }

        private void AddDealerUserProfiles(CRMDbContext context)
        {
            // Create Dealer user role
            const string roleName = "Dealer";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);
            context.SaveChanges();
        }

        private void AddFinancialUserProfiles(CRMDbContext context)
        {
            // Create Financial user role
            const string roleName = "Financial";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);
            context.SaveChanges();
        }

        private void AddAdministratorUserProfiles(CRMDbContext context)
        {
            // Create Admin user role
            const string roleName = "Admin";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);
            context.SaveChanges();

            // Create users
            var hasher = new PasswordHasher();
            var user = new User()
            {
                UserName = "Ivaylo",
                PasswordHash = hasher.HashPassword("123456"),
                Email = "Ivaylo@gmail.com",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Ivaylo",
                SecondName = "Iva",
                LastName = "Ivanov",
                Gender = Gender.Male,
                CreatedOn = DateTime.Now,
                LockoutEnabled = true,
                Website = "http://gmail.com",
                Age = 25,
                Town = "Sofia",
                Country = "Bulgaria",
                EnterprisePosition = EnterprisePosition.Admin,
                PhoneNumber = "0891234567",
                PhoneNumberConfirmed = false,
                LastActivities = new List<Activity>(),
                SchedulerTasks = new List<SchedulerTask>()
            };

            // Add user to role and database
            user.Roles.Add(new IdentityUserRole()
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });

            context.Users.AddOrUpdate(user);
            context.SaveChanges();
        }
    }
}
