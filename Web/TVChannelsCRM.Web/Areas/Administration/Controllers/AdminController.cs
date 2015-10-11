namespace TVChannelsCRM.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Admin;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var usernames = this.Data.Users
                .All()
                .Where(u => u.Id != currentUserId)
                .Select(u => u.UserName)
                .ToList();

            return View(usernames);
        }

        public JsonResult ReadUsers([DataSourceRequest] DataSourceRequest request, string searchboxUsers)
        {
            List<UserViewModel> users;
            var currentUserId = this.User.Identity.GetUserId();

            if (string.IsNullOrEmpty(searchboxUsers) || searchboxUsers == "")
            {
                users = this.Data.Users
                 .All()
                 .Select(UserViewModel.FromUser)
                 .Where(u => u.Id != currentUserId)
                 .ToList();
            }
            else
            {
                users = this.Data.Users
                 .All()
                 .Select(UserViewModel.FromUser)
                 .Where(u => u.UserName.Contains(searchboxUsers) && u.Id != currentUserId)
                 .ToList();
            }

            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState["CreatedOn"].Errors.Clear();
            }

            if (user == null || !ModelState.IsValid)
            {
                return Json(new[] { user }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

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

            this.CreateActivity(ActivityType.Create, newUser.Id, ActivityTargetType.User);

            user.Id = newUser.Id;
            user.CreatedOn = newUser.CreatedOn;

            return Json(new[] { user }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            var userFromDb = this.Data.Users
                .All()
                .FirstOrDefault(u => u.Id == user.Id);

            if (!ModelState.IsValid)
            {
                ModelState["CreatedOn"].Errors.Clear();
            }

            if (user == null || !ModelState.IsValid || userFromDb == null)
            {
                return Json((new[] { user }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
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
                //Roles.RemoveUserFromRoles(userFromDb.UserName, Roles.GetRolesForUser(user.UserName));
                //  userFromDb.Roles = new List<IdentityUserRole>();
                ChangeRoleToUser(userFromDb, user.EnterprisePosition);
            }

            this.Data.SaveChanges();
            this.CreateActivity(ActivityType.Edit, userFromDb.Id, ActivityTargetType.User);

            user.CreatedOn = userFromDb.CreatedOn;
            return Json((new[] { user }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            this.Data.Users.Delete(user.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, user.Id.ToString(), ActivityTargetType.User);

            return Json(new[] { user }, JsonRequestBehavior.AllowGet);
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
            var db = new TVChannelsCRM.Data.TVChannelsCRMDbContext();
            var role = db.Roles.FirstOrDefault(r => r.Name == roleName);

            user.Roles.Add(new IdentityUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
        }
    }
}