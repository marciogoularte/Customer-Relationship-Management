namespace CRM.Services.Logic.Services.Administration
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Administration;
    using Data.ViewModels.Contracts.Activities;

    public class ActivitiesServices : IActivitiesServices
    {
        private ICRMData Data { get; set; }

        public ActivitiesServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<ActivityViewModel> GetActivitiesInAdminRole()
        {
            var lastActivities = this.Data.Activities
                .All()
                .ProjectTo<ActivityViewModel>()
                .OrderByDescending(a => a.CreatedOn)
                .ToList();

            return lastActivities;
        }

        public List<ActivityViewModel> GetActivitiesInNormalRole()
        {
            var lastActivities = this.Data.Activities
                    .All()
                    .ProjectTo<ActivityViewModel>()
                    .Where(a => a.TargetType != ActivityTargetType.User && a.Type != ActivityType.Restore)
                    .OrderByDescending(a => a.CreatedOn)
                    .ToList();

            return lastActivities;
        }
    }
}
