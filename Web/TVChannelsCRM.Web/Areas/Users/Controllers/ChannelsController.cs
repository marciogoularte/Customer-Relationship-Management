﻿namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

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

        public JsonResult ReadChannels([DataSourceRequest] DataSourceRequest request, string searchbox, int providerId)
        {
            List<ChannelViewModel> channels;
            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                channels = this.Data.Channels
                    .All()
                    .Select(ChannelViewModel.FromChannel)
                    .Where(c => c.ProviderId == providerId)
                    .ToList();
            }
            else
            {
                channels = this.Data.Channels
                    .All()
                    .Select(ChannelViewModel.FromChannel)
                    .Where(c => c.Name.Contains(searchbox) && c.ProviderId == providerId)
                    .ToList();
            }

            return Json(channels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateChannel([DataSourceRequest]  DataSourceRequest request, ChannelViewModel channel, int currentProviderId)
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
                LogoLink = channel.LogoLink,
                CreatedOn = DateTime.Now,
                ProviderId = currentProviderId,
                Comments = channel.Comments + "\n"
            };

            if (string.IsNullOrEmpty(newChannel.EpgSource) || newChannel.EpgSource == "")
            {
                newChannel.Website = "#";
            }
            if (string.IsNullOrEmpty(newChannel.Website) || newChannel.Website == "")
            {
                newChannel.Website = "#";
            }
            if (string.IsNullOrEmpty(newChannel.Presentation) || newChannel.Presentation == "")
            {
                newChannel.Presentation = "#";
            }
            if (string.IsNullOrEmpty(newChannel.ContractTemplate) || newChannel.ContractTemplate == "")
            {
                newChannel.Presentation = "#";
            }
            if (string.IsNullOrEmpty(newChannel.LogoLink) || newChannel.LogoLink == "")
            {
                newChannel.Presentation = "#";
            }

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

            if (channel == null || !ModelState.IsValid || channelFromDb == null)
            {
                return Json(new[] { channel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            channelFromDb.Name = channel.Name;
            channelFromDb.ReveivingOptions = channel.ReveivingOptions;
            channelFromDb.SatelliteData = channel.SatelliteData;
            channelFromDb.Comments = channel.Comments + "\n";

            if (string.IsNullOrEmpty(channel.EpgSource) || channel.EpgSource == "")
            {
                channelFromDb.EpgSource = "#";
            }
            else
            {
                channelFromDb.EpgSource = channel.EpgSource;
            }

            if (string.IsNullOrEmpty(channel.Website) || channel.Website == "")
            {
                channelFromDb.Website = "#";
            }
            else
            {
                channelFromDb.Website = channel.Website;
            }

            if (string.IsNullOrEmpty(channel.Presentation) || channel.Presentation == "")
            {
                channelFromDb.Presentation = "#";
            }
            else
            {
                channelFromDb.Presentation = channel.Presentation;
            }

            if (string.IsNullOrEmpty(channel.ContractTemplate) || channel.ContractTemplate == "")
            {
                channelFromDb.ContractTemplate = "#";
            }
            else
            {
                channelFromDb.ContractTemplate = channel.ContractTemplate;
            }

            if (string.IsNullOrEmpty(channel.LogoLink) || channel.LogoLink == "")
            {
                channelFromDb.LogoLink = "#";
            }
            else
            {
                channelFromDb.LogoLink = channel.LogoLink;
            }

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
    }
}