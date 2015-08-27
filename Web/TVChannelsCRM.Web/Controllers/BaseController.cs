namespace TVChannelsCRM.Web.Controllers
{
    using System.Web.Mvc;

    using Data;

    public abstract class BaseController : Controller
    {
        protected ITVChannelsCRMData Data { get; set; }

        protected BaseController(ITVChannelsCRMData data)
        {
            this.Data = data;
        }
    }
}