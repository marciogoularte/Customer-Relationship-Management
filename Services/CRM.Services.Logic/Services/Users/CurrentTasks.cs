using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Services.Data.ViewModels.Users.CurrentTasks;

namespace CRM.Services.Logic.Services.Users
{
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
