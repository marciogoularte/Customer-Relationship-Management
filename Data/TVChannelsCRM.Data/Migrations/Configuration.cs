<<<<<<< HEAD
=======
using System.Collections.Generic;

>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
namespace TVChannelsCRM.Data.Migrations
{
    using System;
    using System.Linq;
<<<<<<< HEAD
    using System.Collections.Generic;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Data;
    using Models;
<<<<<<< HEAD
    using Common.Statics;
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

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
<<<<<<< HEAD
            //this.AddClientsAndProviders(context);
            
            this.AddAdministratorUserProfiles(context);

            //this.AddContractTemplates(context);
=======
            this.AddAdministratorUserProfiles(context);
            //this.AddClientsAndProviders(context);
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
        }

        private void AddClientsAndProviders(TVChannelsCRMDbContext context)
        {
            //for (int i = 100; i < 200; i++)
            //{
            //    var provider = new Provider()
            //    {
            //        Name = "Name " + (i * 23),
            //        Type = TypeOfCompany.OOD,
            //        Address = "Al Malinow" + (i * 23),
            //        Commission = (i * 23).ToString(),
            //        ContactPerson = "Pesho" + (i * 23),
            //        Term = "Term" + (i * 23),
            //        CPS = "CPS" + (i * 23),
<<<<<<< HEAD
            //        Uic = "Uic" + (i * 23),
=======
            //        Eik = "Eik" + (i * 23),
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
            //        ResidenceAndAddress = " ResidenceAndAddress " + (i * 20),
            //        NetworkPage = "NetworkPage" + i * 3.6,
            //        PhoneNumber = "0891929384",
            //        Email = "Pesho" + i + "@gmail.com"
            //    };

            //    context.Providers.AddOrUpdate(provider);
            //}




            //for (int i = 100; i < 200; i++)
            //{
            //    var firstClient = new Client()
            //    {
            //        IsActive = true,
            //        ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i),
            //        ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i),
            //        Mg = "MG" + i*0.4,
            //        IrdCard = i*19.00002 + "IrdCard",
            //        Invoicing = i + "asdasd",
            //        DateOfSigning = DateTime.Now,
            //        DateOfExpiring = DateTime.Now,
            //        Currency = (i*20).ToString(),
            //        InvoicesIssued = i + "Some Random" + (i/19.2),
            //        PaymentsReceived = i.ToString(),
            //        Contract = "Contract" + i
            //    };

            //    var secondClient = new Client()
            //    {
            //        IsActive = false,
            //        ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i),
            //        ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i),
            //        Mg = "MG" + i*0.4,
            //        IrdCard = i*19.00002 + "IrdCard",
            //        Invoicing = i + "asdasd",
            //        DateOfSigning = DateTime.Now,
            //        DateOfExpiring = DateTime.Now,
            //        Currency = (i*20).ToString(),
            //        InvoicesIssued = i + "Some Random" + (i/19.2),
            //        PaymentsReceived = i.ToString(),
            //        Contract = "Contract" + i
            //    };

            //    context.Clients.AddOrUpdate(firstClient);
            //    context.Clients.AddOrUpdate(secondClient);
<<<<<<< HEAD
            //    context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

            // }
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
<<<<<<< HEAD
            context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

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
<<<<<<< HEAD
            //context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
            context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
            //context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
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
<<<<<<< HEAD
            context.SaveChanges();
=======
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8

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
<<<<<<< HEAD
            context.SaveChanges();

            //var secondUser = new User()
            //{
            //    UserName = "Nikolay",
            //    PasswordHash = hasher.HashPassword("123456"),
            //    Email = "Nikolay@gmail.com",
            //    EmailConfirmed = false,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    FirstName = "Nikolay",
            //    SecondName = "Petrov",
            //    LastName = "Dermendzhiev",
            //    Gender = Gender.Male,
            //    CreatedOn = DateTime.Now,
            //    LockoutEnabled = true,
            //    Age = 10,
            //    Town = "Panagyurishte",
            //    Country = "Bulgaria",
            //    Website = "http://gmail.com",
            //    EnterprisePosition = EnterprisePosition.Admin,
            //    PhoneNumber = "0897654321",
            //    PhoneNumberConfirmed = false,
            //    LastActivities = new List<Activity>(),
            //    SchedulerTasks = new List<SchedulerTask>()
            //};
            //
            //// Add user to role and database
            //secondUser.Roles.Add(new IdentityUserRole()
            //{
            //    RoleId = userRole.Id,
            //    UserId = user.Id
            //});
            //
            //context.Users.AddOrUpdate(secondUser);
            //context.SaveChanges();
        }

        //private void AddContractTemplates(TVChannelsCRMDbContext context)
        //{
        //    // TODO: Set correct TargetContractTemplate for all contracts (target: client or provider)
        //    var boxContractTemplate = new ContractTemplate()
        //    {
        //        Name = "BOX",
        //        Html = ContractTemplatesHtml.BoxContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(boxContractTemplate);

        //    var ebuLaContractTemplate = new ContractTemplate()
        //    {
        //        Name = "EBU_LA",
        //        Html = ContractTemplatesHtml.EbuLaContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(ebuLaContractTemplate);

        //    var ectvEnContractTemplate = new ContractTemplate()
        //    {
        //        Name = "ECTV-EN",
        //        Html = ContractTemplatesHtml.EctvEnContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(ectvEnContractTemplate);

        //    var ectvEnRuContractTemplate = new ContractTemplate()
        //    {
        //        Name = "ECTV-EN-RU",
        //        Html = ContractTemplatesHtml.EctvEnRuContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(ectvEnRuContractTemplate);

        //    var fashiononeContractTemplate = new ContractTemplate()
        //    {
        //        Name = "Fashionone",
        //        Html = ContractTemplatesHtml.FashiononeContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(fashiononeContractTemplate);

        //    var fcwContractTemplate = new ContractTemplate()
        //    {
        //        Name = "FCW",
        //        Html = ContractTemplatesHtml.FcwContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(fcwContractTemplate);

        //    var fishingContractTemplate = new ContractTemplate()
        //    {
        //        Name = "FISHING",
        //        Html = ContractTemplatesHtml.FishingContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(fishingContractTemplate);

        //    var imagineContractTemplate = new ContractTemplate()
        //    {
        //        Name = "IMAGINE",
        //        Html = ContractTemplatesHtml.ImagineContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(imagineContractTemplate);

        //    var mixPackContractTemplate = new ContractTemplate()
        //    {
        //        Name = "MixPack",
        //        Html = ContractTemplatesHtml.MixPackContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(mixPackContractTemplate);

        //    var movieSelsContractTemplate = new ContractTemplate()
        //    {
        //        Name = "MOVIE SELS",
        //        Html = ContractTemplatesHtml.MovieSelsContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(movieSelsContractTemplate);

        //    var moviestarContractTemplate = new ContractTemplate()
        //    {
        //        Name = "Moviestar",
        //        Html = ContractTemplatesHtml.MoviestarContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(moviestarContractTemplate);

        //    var romaContractTemplate = new ContractTemplate()
        //    {
        //        Name = "ROMA",
        //        Html = ContractTemplatesHtml.RomaContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(romaContractTemplate);

        //    var sctContractTemplate = new ContractTemplate()
        //    {
        //        Name = "SCT",
        //        Html = ContractTemplatesHtml.SctContractTemplateHtml,
        //        Target = TargetContractTemplate.Client
        //    };
        //    context.ContractTemplates.AddOrUpdate(sctContractTemplate);

        //    var superOneContractTemplate = new ContractTemplate()
        //    {
        //        Name = "SuperOne",
        //        Html = ContractTemplatesHtml.SuperOneContractTemplateHtml,
        //        Target = TargetContractTemplate.Provider
        //    };
        //    context.ContractTemplates.AddOrUpdate(superOneContractTemplate);

        //    var theWorldContractTemplate = new ContractTemplate()
        //    {
        //        Name = "The_World",
        //        Html = ContractTemplatesHtml.TheWorldContractTemplateHtml,
        //        Target = TargetContractTemplate.Provider
        //    };
        //    context.ContractTemplates.AddOrUpdate(theWorldContractTemplate);

        //    context.SaveChanges();
        //}
    }
}
=======

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
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
