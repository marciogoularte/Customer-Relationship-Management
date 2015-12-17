using CRM.Data;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
    public class FinanceServices : IFinanceServices
    {
        private ICRMData Data { get; set; }

        public FinanceServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
