namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;
    using Web.Controllers;
    using ViewModels.Providers;

    public class ChannelsController : BaseController
    {
        public ChannelsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllChannelsData(int providerId)
        {
            return PartialView("_Channels", providerId);
        }

        public ActionResult GetChannelsNames([DataSourceRequest]DataSourceRequest request)
        {
            var channelsNames = this.Data.Channels
                .All()
                .Where(c => c.Provider != null)
                .Select(c => c.Name)
                .ToList();

            return Json(channelsNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId)
        {
            var channels = this.Data.Channels
                .All()
                .Select(ChannelViewModel.FromChannel)
                .Where(c => c.Name.Contains(searchbox) && c.Provider.Id == providerId)
                .ToList();

            return Json(channels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadChannels([DataSourceRequest] DataSourceRequest request, int providerId)
        {
            var channels = this.Data.Channels
                .All()
                .Where(c => c.Provider.Id == providerId)
                .Select(ChannelViewModel.FromChannel)
                .ToList();

            return Json(channels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateChannel([DataSourceRequest]  DataSourceRequest request, ChannelViewModel channel, int providerId)
        {
            if (channel == null || !ModelState.IsValid)
            {
                return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var newChannel = new Channel
            {
                Name = channel.Name,
                ReveivingOptions = channel.ReveivingOptions,
                SatelliteData = channel.SatelliteData,
                EpgSource = channel.EpgSource,
                Website = channel.Website,
                Presentation = channel.Presentation,
                ContractTemplate = channel.ContractTemplate,
                CreatedOn = DateTime.Now,
                ProviderId = providerId,
                Comments = channel.Comments + "\n"
            };

            this.Data.Channels.Add(newChannel);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newChannel.Id.ToString(), ActivityTargetType.Channel);
            channel.Id = newChannel.Id;

            return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        } 
        
        [HttpGet]
        public async Task<ActionResult> ChannelDetails(int channelId)
        {
            var channel = await this.Data.Channels
                .All()
                .Select(ChannelViewModel.FromChannel)
                .FirstOrDefaultAsync(c => c.Id == channelId);

            return View(channel);
        }
       
        public JsonResult UpdateChannel([DataSourceRequest] DataSourceRequest request, ChannelViewModel channel)
        {
            var channelFromDb = this.Data.Channels
                .All()
                  .FirstOrDefault(c => c.Id == channel.Id);

            if (channel == null || !ModelState.IsValid || channelFromDb == null)
            {
                return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            channelFromDb.Name = channel.Name;
            channelFromDb.ReveivingOptions = channel.ReveivingOptions;
            channelFromDb.SatelliteData = channel.SatelliteData;
            channelFromDb.EpgSource = channel.EpgSource;
            channelFromDb.Website = channel.Website;
            channelFromDb.Presentation = channel.Presentation;
            channelFromDb.ContractTemplate = channel.ContractTemplate;
            channelFromDb.Comments = channel.Comments + "\n";

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, channelFromDb.Id.ToString(), ActivityTargetType.Channel);

            return Json((new[] { channel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyChannel([DataSourceRequest] DataSourceRequest request, ChannelViewModel channel)
        {
            this.Data.Channels.Delete(channel.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, channel.Id.ToString(), ActivityTargetType.Channel);

            return Json(new[] { channel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        private void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            // If activities are more than 200 just override the oldest one so will not have more than 200 activities
            if (this.Data.Activities.All().Count() >= 200)
            {
                var activity = this.Data.Activities.All().OrderBy(a => a.CreatedOn).FirstOrDefault();
                activity.UserId = loggedUserId;
                activity.Type = type;
                activity.TargetId = targetId;
                activity.TargetType = targetType;
                activity.CreatedOn = DateTime.Now;
            }
            else
            {
                var activity = new Activity()
                {
                    UserId = loggedUserId,
                    Type = type,
                    TargetId = targetId,
                    TargetType = targetType
                };

                this.Data.Activities.Add(activity);
            }

            this.Data.SaveChanges();
        }
    }
}