namespace CRM.Web.Areas.Contractors.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Web.Controllers;
    using Services.Logic.Contracts.Contractors;
    using Services.Data.ViewModels.Contracts.Providers;

    [Authorize]
    public class ChannelsController : BaseController
    {
        private readonly IChannelsServices channels;

        public ChannelsController(IChannelsServices channels)
        {
            this.channels = channels;
        }

        [HttpGet]
        public ActionResult AllChannelsData(int providerId)
        {
            return PartialView("_Channels", providerId);
        }

        public ActionResult GetChannelsNames([DataSourceRequest]DataSourceRequest request)
        {
            var channelsNames = this.channels.GetChannelsNames();

            return Json(channelsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadChannels([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId, bool? showAll)
        {
            var readChannels = (showAll != null) ? (this.channels.ReadChannels(searchbox, providerId, (bool)showAll)) : (this.channels.ReadChannels(searchbox, providerId, false));

            return Json(readChannels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateChannel([DataSourceRequest]  DataSourceRequest request, ChannelViewModel channel, int currentProviderId)
        {
            if (channel == null || !ModelState.IsValid)
            {
                return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdChannel = this.channels.CreateChannel(channel, currentProviderId);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdChannel.Id.ToString(), ActivityTargetType.Channel, loggedUserId);

            return Json(new[] { createdChannel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ChannelDetails(int channelId)
        {
            var channelDetails = this.channels.ChannelDetails(channelId);

            return View(channelDetails);
        }

        [HttpGet]
        public ActionResult ViewOperators(int channelId)
        {
            var clients = this.channels.ViewOperators(channelId);

            return View("Clients", clients);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateChannel([DataSourceRequest] DataSourceRequest request, ChannelViewModel channel)
        {
            foreach (var propertyName in ModelState.Select(modelError => modelError.Key))
            {
                if (propertyName.Contains("Client"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
                else if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
            }

            if (channel == null || !ModelState.IsValid)
            {
                return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedChannel = this.channels.UpdateChannel(channel);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedChannel.Id.ToString(), ActivityTargetType.Channel, loggedUserId);

            return Json((new[] { channel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyChannel([DataSourceRequest] DataSourceRequest request, ChannelViewModel channel)
        {
            var deletedChannel = this.channels.DestroyChannel(channel);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedChannel.Id.ToString(), ActivityTargetType.Channel, loggedUserId);

            return Json(new[] { channel }, JsonRequestBehavior.AllowGet);
        }
    }
}