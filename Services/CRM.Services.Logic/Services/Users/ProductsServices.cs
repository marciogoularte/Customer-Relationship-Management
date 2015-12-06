namespace CRM.Services.Logic.Services.Users
{
    using CRM.Data;
    using Contracts.Users;

    public class ProductsServices : IProductsServices
    {
        private ICRMData Data { get; set; }

        public ProductsServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
