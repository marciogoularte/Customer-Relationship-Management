namespace CRM.Services.Logic.Services.Contractors
{
    using CRM.Data;
    using Contracts.Contractors;

    public class MarketingServices : IMarketingServices
    {
        private ICRMData Data { get; set; }

        public MarketingServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
