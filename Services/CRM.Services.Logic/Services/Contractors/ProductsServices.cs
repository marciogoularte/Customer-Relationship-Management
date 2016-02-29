namespace CRM.Services.Logic.Services.Contractors
{
    using CRM.Data;
    using Contracts.Contractors;

    public class ProductsServices : IProductsServices
    {
        private ICRMData Data { get; set; }

        public ProductsServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
