namespace CRM.Services.Logic.Services.Users
{
    using CRM.Data;
    using Contracts.Users;

    public class FinanceServices : IFinanceServices
    {
        private ICRMData Data { get; set; }

        public FinanceServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
