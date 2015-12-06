namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Web.Controllers;
    using Services.Logic.Contracts.Users;

    public class ProductsController : BaseController
    {
        private readonly IProductsServices products;

        public ProductsController(IProductsServices products)
        {
            this.products = products;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}