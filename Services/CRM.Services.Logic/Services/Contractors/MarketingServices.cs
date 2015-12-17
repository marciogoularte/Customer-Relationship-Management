using CRM.Data;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
    public class MarketingServices : IMarketingServices
    {
        private ICRMData Data { get; set; }

        public MarketingServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
