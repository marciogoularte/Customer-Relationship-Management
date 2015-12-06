namespace CRM.Services.Logic.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Administration;
    using Data.ViewModels.Administration.Admin;

    public class AdminServices : IAdminServices
    {
        private ICRMData Data { get; set; }

        public AdminServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index(string loggedUserId)
        {
            var usernames = this.Data.Users
                .All()
                .Where(u => u.Id != loggedUserId)
                .Select(u => u.UserName)
                .ToList();

            return usernames;
        }

        public List<UserViewModel> ReadUsers(string searchboxUsers, string loggedUserId)
        {
            List<UserViewModel> users;

            if (string.IsNullOrEmpty(searchboxUsers) || searchboxUsers == "")
            {
                users = this.Data.Users
                 .All()
                 .Select(UserViewModel.FromUser)
                 .Where(u => u.Id != loggedUserId)
                 .ToList();
            }
            else
            {
                users = this.Data.Users
                 .All()
                 .Select(UserViewModel.FromUser)
                 .Where(u => u.UserName.Contains(searchboxUsers) && u.Id != loggedUserId)
                 .ToList();
            }

            return users;
        }

        public UserViewModel CreateUser(UserViewModel user)
        {
            var hasher = new PasswordHasher();
            var newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = hasher.HashPassword(user.PasswordHash),
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                LastName = user.LastName,
                Gender = user.Gender,
                Age = user.Age,
                Town = user.Town,
                Country = user.Country,
                CreatedOn = DateTime.Now,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                PhoneNumber = user.PhoneNumber,
                Website = user.Website
            };

            ChangeRoleToUser(newUser, user.EnterprisePosition);

            this.Data.Users.Add(newUser);
            this.Data.SaveChanges();

            user.Id = newUser.Id;
            user.CreatedOn = newUser.CreatedOn;

            return user;
        }

        public UserViewModel UpdateUser(UserViewModel user)
        {
            var userFromDb = this.Data.Users
                .All()
                .FirstOrDefault(u => u.Id == user.Id);

            if (user == null || userFromDb == null)
            {
                return user;
            }

            if (user.PasswordHash != null)
            {
                if (user.PasswordHash != userFromDb.PasswordHash)
                {
                    var hasher = new PasswordHasher();
                    userFromDb.PasswordHash = hasher.HashPassword(user.PasswordHash);
                }
            }

            userFromDb.UserName = user.UserName;
            userFromDb.Email = user.Email;
            userFromDb.FirstName = user.FirstName;
            userFromDb.SecondName = user.SecondName;
            userFromDb.LastName = user.LastName;
            userFromDb.Gender = user.Gender;
            userFromDb.Age = user.Age;
            userFromDb.Town = user.Town;
            userFromDb.Country = user.Country;
            userFromDb.PhoneNumber = user.PhoneNumber;
            userFromDb.Website = user.Website;

            if (userFromDb.EnterprisePosition != user.EnterprisePosition)
            {
                userFromDb.Roles.Clear();
                ChangeRoleToUser(userFromDb, user.EnterprisePosition);
            }

            this.Data.SaveChanges();

            user.Id = userFromDb.Id;
            user.CreatedOn = userFromDb.CreatedOn;

            return user;
        }

        public UserViewModel DestroyUser(UserViewModel user)
        {
            this.Data.Users.Delete(user.Id);
            this.Data.SaveChanges();

            return user;
        }

        private void ChangeRoleToUser(User user, EnterprisePosition position)
        {
            var roleName = "";
            switch (position)
            {
                case EnterprisePosition.Financial:
                    user.EnterprisePosition = EnterprisePosition.Financial;
                    roleName = "Financial";
                    break;
                case EnterprisePosition.Dealer:
                    user.EnterprisePosition = EnterprisePosition.Dealer;
                    roleName = "Dealer";
                    break;
                default:
                    user.EnterprisePosition = EnterprisePosition.Admin;
                    roleName = "Admin";
                    break;
            }

            // TODO: Remove db context and find way for manipulating data in other way
            var db = new CRMDbContext();
            var role = db.Roles.FirstOrDefault(r => r.Name == roleName);

            user.Roles.Add(new IdentityUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
        }
    }
}
