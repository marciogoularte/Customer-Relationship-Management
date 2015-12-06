namespace CRM.Services.Logic.Services.Users
{
    using CRM.Data;
    using Contracts.Users;

    public class MarketingServices : IMarketingServices
    {
        private ICRMData Data { get; set; }

        public MarketingServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
