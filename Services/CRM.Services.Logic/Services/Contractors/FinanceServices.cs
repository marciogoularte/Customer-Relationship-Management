namespace CRM.Services.Logic.Services.Contractors
{
    using CRM.Data;
    using Contracts.Contractors;

    public class FinanceServices : IFinanceServices
    {
        private ICRMData Data { get; set; }

        public FinanceServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
