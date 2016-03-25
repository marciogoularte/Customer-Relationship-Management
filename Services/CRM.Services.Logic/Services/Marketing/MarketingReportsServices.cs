namespace CRM.Services.Logic.Services.Marketing
{
    using CRM.Data;
    using Contracts.Marketing;

    public class MarketingReportsServices : IMarketingReportsServices
    {
        private ICRMData Data { get; }

        public MarketingReportsServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
