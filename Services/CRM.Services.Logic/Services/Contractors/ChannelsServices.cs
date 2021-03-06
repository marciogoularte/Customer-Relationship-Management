﻿namespace CRM.Services.Logic.Services.Contractors
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Contractors;
    using Data.ViewModels.Contracts.Clients;
    using Data.ViewModels.Contracts.Providers;

    public class ChannelsServices : IChannelsServices
    {
        private ICRMData Data { get; set; }

        public ChannelsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetChannelsNames()
        {
            var channelsNames = this.Data.Channels
                .All()
                .Where(c => c.Provider != null)
                .Select(c => c.Name)
                .ToList();

            return channelsNames;
        }

        public List<ChannelViewModel> ReadChannels(string searchbox, int providerId, bool showAll)
        {
            List<ChannelViewModel> channels;

            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                if (showAll == false)
                {
                    channels = this.Data.Channels
                        .All()
                        .ProjectTo<ChannelViewModel>()
                        .Where(c => c.ProviderId == providerId && c.IsVisible)
                        .ToList();
                }
                else
                {
                    channels = this.Data.Channels
                        .All()
                        .ProjectTo<ChannelViewModel>()
                        .Where(c => c.ProviderId == providerId)
                        .ToList();
                }
            }
            else
            {
                channels = this.Data.Channels
                    .All()
                    .ProjectTo<ChannelViewModel>()
                    .Where(c => c.Name.Contains(searchbox) && c.ProviderId == providerId)
                    .ToList();
            }

            return channels;
        }

        public ChannelViewModel CreateChannel(ChannelViewModel channel, int currentProviderId)
        {
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
                Comments = channel.Comments,
                IsVisible = channel.IsVisible
            };

            if (string.IsNullOrEmpty(newChannel.EpgSource) && string.IsNullOrEmpty(channel.EpgSource))
            {
                newChannel.EpgSource = "#";
            }
            if (string.IsNullOrEmpty(newChannel.Website) && string.IsNullOrEmpty(channel.Website))
            {
                newChannel.Website = "#";
            }
            if (string.IsNullOrEmpty(newChannel.Presentation) && string.IsNullOrEmpty(channel.Presentation))
            {
                newChannel.Presentation = "#";
            }
            if (string.IsNullOrEmpty(newChannel.ContractTemplate) && string.IsNullOrEmpty(channel.ContractTemplate))
            {
                newChannel.ContractTemplate = "#";
            }
            if (string.IsNullOrEmpty(newChannel.LogoLink) && string.IsNullOrEmpty(channel.LogoLink))
            {
                newChannel.LogoLink = "#";
            }

            this.Data.Channels.Add(newChannel);
            this.Data.Channels.SaveChanges();

            channel.Id = newChannel.Id;

            return channel;
        }

        public ChannelViewModel ChannelDetails(int channelId)
        {
            var channel = this.Data.Channels
                .All()
                .ProjectTo<ChannelViewModel>()
                .FirstOrDefault(c => c.Id == channelId);

            return channel;
        }

        public List<ClientViewModel> ViewOperators(int channelId)
        {
            var contracts = this.Data.ClientContracts
                .All()
                .Where(c => c.Channels.Select(ch => ch.Id).Contains(channelId))
                .ToList();

            var clients = new List<ClientViewModel>();

            foreach (var contractClients in contracts.Select(contract => this.Data.Clients
                .All()
                .Where(c => c.Contracts.Select(co => co.Id).Contains(contract.Id))
                .ProjectTo<ClientViewModel>()
                .ToList()))
            {
                clients.AddRange(contractClients);
            }

            return clients;
        }

        public ChannelViewModel UpdateChannel(ChannelViewModel channel)
        {
            var channelFromDb = this.Data.Channels
                .All()
                  .FirstOrDefault(c => c.Id == channel.Id);

            channelFromDb.Name = channel.Name;
            channelFromDb.ReveivingOptions = channel.ReveivingOptions;
            channelFromDb.SatelliteData = channel.SatelliteData;
            channelFromDb.Comments = channel.Comments;
            channelFromDb.IsVisible = channel.IsVisible;

            if (string.IsNullOrEmpty(channel.EpgSource) && string.IsNullOrEmpty(channelFromDb.EpgSource))
            {
                channelFromDb.EpgSource = "#";
            }
            else
            {
                channelFromDb.EpgSource = channel.EpgSource;
            }

            if (string.IsNullOrEmpty(channel.Website) && string.IsNullOrEmpty(channelFromDb.Website))
            {
                channelFromDb.Website = "#";
            }
            else
            {
                channelFromDb.Website = channel.Website;
            }

            if (string.IsNullOrEmpty(channel.Presentation) && string.IsNullOrEmpty(channelFromDb.Presentation))
            {
                channelFromDb.Presentation = "#";
            }
            else
            {
                channelFromDb.Presentation = channel.Presentation;
            }

            if (string.IsNullOrEmpty(channel.ContractTemplate) && string.IsNullOrEmpty(channelFromDb.ContractTemplate))
            {
                channelFromDb.ContractTemplate = "#";
            }
            else
            {
                channelFromDb.ContractTemplate = channel.ContractTemplate;
            }

            if (string.IsNullOrEmpty(channel.LogoLink) && string.IsNullOrEmpty(channel.LogoLink))
            {
                channelFromDb.LogoLink = "#";
            }
            else
            {
                channelFromDb.LogoLink = channel.LogoLink;
            }

            this.Data.Channels.SaveChanges();

            return channel;
        }

        public ChannelViewModel DestroyChannel(ChannelViewModel channel)
        {
            this.Data.Channels.Delete(channel.Id);
            this.Data.Channels.SaveChanges();

            return channel;
        }
    }
}
