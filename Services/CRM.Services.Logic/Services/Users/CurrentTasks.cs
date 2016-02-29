namespace CRM.Services.Logic.Services.Users
{
    using System.Collections.Generic;

    using CRM.Data;
    using Data.ViewModels.Users.CurrentTasks;

    public class CurrentTasks
    {
        private ICRMData Data { get; set; }

        public CurrentTasks(ICRMData data)
        {
            this.Data = data;
        }

        public List<TodayPayment> TodayPayments()
        {
            return new List<TodayPayment>();
        }
    }
}
