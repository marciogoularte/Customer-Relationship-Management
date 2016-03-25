namespace CRM.Services.Logic.Services.Users
{
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using Contracts.Users;
    using Data.ViewModels.Users.ManageUser;

    public class ManageUserServices : IManageUserServices
    {
        private ICRMData Data { get; set; }

        public ManageUserServices(ICRMData data)
        {
            this.Data = data;
        }

        public ManageUserViewModel GetUserData(string userId)
        {
            var user = this.Data.Users
                .All()
                .Where(u => u.Id == userId)
                .ProjectTo<ManageUserViewModel>()
                .FirstOrDefault();

            return user;
        }

        public void ManageData(string userId, ManageUserViewModel user)
        {
            var userFromDb = this.Data.Users
                .GetById(userId);

            userFromDb.FirstName = user.FirstName;
            userFromDb.SecondName = user.SecondName;
            userFromDb.LastName = user.LastName;
            userFromDb.Gender = user.Gender;
            userFromDb.Age = user.Age;
            userFromDb.Town = user.Town;
            userFromDb.Country = user.Country;
            userFromDb.Website = user.Website;

            this.Data.Users.SaveChanges();
        }
    }
}
