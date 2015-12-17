using CRM.Data;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
    public class ProductsServices : IProductsServices
    {
        private ICRMData Data { get; set; }

        public ProductsServices(ICRMData data)
        {
            this.Data = data;
        }
    }
}
