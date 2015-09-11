using System.Collections.Generic;

namespace TVChannelsCRM.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Data;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TVChannelsCRMDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TVChannelsCRMDbContext context)
        {
            if (context.Users.Any() || context.Clients.Any() || context.Providers.Any())
            {
                return;
            }
            //
            //this.AddDealerUserProfiles(context);
            //this.AddFinancialUserProfiles(context);
            this.AddAdministratorUserProfiles(context);
            //this.AddClientsAndProviders(context);
        }

        private void AddClientsAndProviders(TVChannelsCRMDbContext context)
        {
            for (int i = 100; i < 200; i++)
            {
                var provider = new Provider()
                {
                    Name = "Name " + (i * 23),
                    Type = TypeOfCompany.OOD,
                    Address = "Al Malinow" + (i * 23),
                    Commission = (i * 23).ToString(),
                    ContactPerson = "Pesho" + (i * 23),
                    Term = "Term" + (i * 23),
                    CPS = "CPS" + (i * 23),
                    Eik = "Eik" + (i * 23),
                    ResidenceAndAddress = " ResidenceAndAddress " + (i * 20),
                    NetworkPage = "NetworkPage" + i * 3.6,
                    PhoneNumber = "0891929384",
                    Email = "Pesho" + i + "@gmail.com"
                };

                context.Providers.AddOrUpdate(provider);
            }




            for (int i = 100; i < 200; i++)
            {
                var firstClient = new Client()
                {
                    IsActive = true,
                    ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i),
                    ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i),
                    Mg = "MG" + i*0.4,
                    IrdCard = i*19.00002 + "IrdCard",
                    Invoicing = i + "asdasd",
                    DateOfSigning = DateTime.Now,
                    DateOfExpiring = DateTime.Now,
                    Currency = (i*20).ToString(),
                    InvoicesIssued = i + "Some Random" + (i/19.2),
                    PaymentsReceived = i.ToString(),
                    Contract = "Contract" + i
                };

                var secondClient = new Client()
                {
                    IsActive = false,
                    ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i),
                    ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i),
                    Mg = "MG" + i*0.4,
                    IrdCard = i*19.00002 + "IrdCard",
                    Invoicing = i + "asdasd",
                    DateOfSigning = DateTime.Now,
                    DateOfExpiring = DateTime.Now,
                    Currency = (i*20).ToString(),
                    InvoicesIssued = i + "Some Random" + (i/19.2),
                    PaymentsReceived = i.ToString(),
                    Contract = "Contract" + i
                };

                context.Clients.AddOrUpdate(firstClient);
                context.Clients.AddOrUpdate(secondClient);

            }
        }

        private void AddDealerUserProfiles(TVChannelsCRMDbContext context)
        {
            // Create Dealer user role
            const string roleName = "Dealer";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);

            //// Create user
            //var hasher = new PasswordHasher();
            //var user = new User()
            //    {
            //        UserName = "Dealer",
            //        PasswordHash = hasher.HashPassword("123456"),
            //        Email = "Dealer@abv.bg",
            //        EmailConfirmed = false,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        FirstName = "Dealer",
            //        SecondName = "Dealer",
            //        LastName = "Dealer",
            //        Gender = Gender.Male,
            //        CreatedOn = DateTime.Now,
            //        LockoutEnabled = true,
            //        Age = 10,
            //        Town = "Sofia",
            //        Country = "Bulgaria",
            //        EnterprisePosition = EnterprisePosition.Dealer,
            //        PhoneNumber = "0891234567",
            //        PhoneNumberConfirmed = false
            //    };
            //
            //// Add user to role and database
            //user.Roles.Add(new IdentityUserRole()
            //{
            //    RoleId = userRole.Id,
            //    UserId = user.Id
            //});
            //
            //context.Users.AddOrUpdate(user);
        }

        private void AddFinancialUserProfiles(TVChannelsCRMDbContext context)
        {
            // Create Financial user role
            const string roleName = "Financial";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);
            //
            //// Create user
            //var hasher = new PasswordHasher();
            //var user = new User()
            //{
            //    UserName = "Financial",
            //    PasswordHash = hasher.HashPassword("123456"),
            //    Email = "Financial@abv.bg",
            //    EmailConfirmed = false,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    FirstName = "Financial",
            //    SecondName = "Financial",
            //    LastName = "Financial",
            //    Gender = Gender.Female,
            //    CreatedOn = DateTime.Now,
            //    LockoutEnabled = true,
            //    Age = 10,
            //    Town = "Sofia",
            //    Country = "Bulgaria",
            //    EnterprisePosition = EnterprisePosition.Financial,
            //    PhoneNumber = "0891234567",
            //    PhoneNumberConfirmed = false
            //};
            //
            //// Add user to role and database
            //user.Roles.Add(new IdentityUserRole()
            //{
            //    RoleId = userRole.Id,
            //    UserId = user.Id
            //});
            //
            //context.Users.AddOrUpdate(user);
        }

        private void AddAdministratorUserProfiles(TVChannelsCRMDbContext context)
        {
            // Create Admin user role
            const string roleName = "Admin";
            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);

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
                LastActivities= new List<Activity>(),
                SchedulerTasks = new List<SchedulerTask>()
            };

            // Add user to role and database
            user.Roles.Add(new IdentityUserRole()
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });

            context.Users.AddOrUpdate(user);

            var secondUser = new User()
            {
                UserName = "Nikolay",
                PasswordHash = hasher.HashPassword("123456"),
                Email = "Nikolay@gmail.com",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Nikolay",
                SecondName = "Petrov",
                LastName = "Dermendzhiev",
                Gender = Gender.Male,
                CreatedOn = DateTime.Now,
                LockoutEnabled = true,
                Age = 10,
                Town = "Panagyurishte",
                Country = "Bulgaria",
                Website = "http://gmail.com",
                EnterprisePosition = EnterprisePosition.Admin,
                PhoneNumber = "0897654321",
                PhoneNumberConfirmed = false,
                LastActivities = new List<Activity>(),
                SchedulerTasks = new List<SchedulerTask>()
            };

            // Add user to role and database
            secondUser.Roles.Add(new IdentityUserRole()
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });

            context.Users.AddOrUpdate(secondUser);
        }
    }
}
