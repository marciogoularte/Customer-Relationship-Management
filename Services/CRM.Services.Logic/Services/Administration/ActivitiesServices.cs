﻿namespace CRM.Services.Logic.Services.Administration
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Administration;
    using Data.ViewModels.Users.Activities;

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
                .Select(ActivityViewModel.FromActivity)
                .OrderByDescending(a => a.CreatedOn)
                .ToList();

            return lastActivities;
        }

        public List<ActivityViewModel> GetActivitiesInNormalRole()
        {
            var lastActivities = this.Data.Activities
                    .All()
                    .Select(ActivityViewModel.FromActivity)
                    .Where(a => a.TargetType != ActivityTargetType.User && a.Type != ActivityType.Restore)
                    .OrderByDescending(a => a.CreatedOn)
                    .ToList();

            return lastActivities;
        }
    }
}
