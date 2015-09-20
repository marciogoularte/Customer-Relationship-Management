namespace TVChannelsCRM.Web.Areas.Administration.Controllers
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
    using Users.ViewModels.Clients;
    using Users.ViewModels.Providers;
    using Users.ViewModels.Contracts;

    [Authorize(Roles = "Admin")]
    public class DeletedItemsController : BaseController
    {
        public DeletedItemsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public async Task<ActionResult> DeletedClients()
        {
            var clientsNames = await this.Data.Clients
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.Name)
                .ToListAsync();

            return View("DeletedClients", clientsNames);
        }

        public async Task<ActionResult> DeletedProviders()
        {
            var providersNames = await this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.IsDeleted == true)
                .Select(p => p.Name)
                .ToListAsync();

            return View("DeletedProviders", providersNames);
        }

        public async Task<ActionResult> DeletedChannels()
        {
            var channelsNames = await this.Data.Channels
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.Name)
                .ToListAsync();

            return View("DeletedChannels", channelsNames);
        }

        public async Task<ActionResult> DeletedClientContracts()
        {
            var contractsNames = await this.Data.ClientContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.TypeOfContract.ToString())
                .ToListAsync();

            return View("DeletedClientContracts", contractsNames);
        }

        public async Task<ActionResult> DeletedProviderContracts()
        {
            var contractsNames = await this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(c => c.TypeOfContract.ToString())
                .ToListAsync();

            return View("DeletedProviderContracts", contractsNames);
        }

        public JsonResult ReadDeletedClients([DataSourceRequest] DataSourceRequest request, string searchboxDeletedClients)
        {
            List<ClientViewModel> clients;
            if (searchboxDeletedClients == "")
            {
                clients = this.Data.Clients
                 .AllWithDeleted()
                 .Where(c => c.IsDeleted == true)
                 .Select(ClientViewModel.FromClient)
                 .ToList();
            }
            else
            {
                clients = this.Data.Clients
                 .AllWithDeleted()
                 .Where(c => c.Name.Contains(searchboxDeletedClients) && c.IsDeleted == true)
                 .Select(ClientViewModel.FromClient)
                 .ToList();
            }

            return Json(clients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedProviders([DataSourceRequest] DataSourceRequest request, string searchboxDeletedProviders)
        {
            List<ProviderViewModel> providers;
            if (searchboxDeletedProviders == "")
            {
                providers = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.IsDeleted == true)
                .Select(ProviderViewModel.FromProvider)
                .ToList();
            }
            else
            {
                providers = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.Name.Contains(searchboxDeletedProviders) && p.IsDeleted == true)
                .Select(ProviderViewModel.FromProvider)
                .ToList();
            }

            return Json(providers.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedChannels([DataSourceRequest] DataSourceRequest request, string searchboxDeletedChannels)
        {
            List<ChannelViewModel> channels;
            if (searchboxDeletedChannels == "")
            {
                channels = this.Data.Channels
                    .AllWithDeleted()
                    .Where(c => c.IsDeleted == true)
                    .Select(ChannelViewModel.FromChannel)
                    .ToList();
            }
            else
            {
                channels = this.Data.Channels
                   .AllWithDeleted()
                   .Where(c => c.Name.Contains(searchboxDeletedChannels) && c.IsDeleted == true)
                   .Select(ChannelViewModel.FromChannel)
                   .ToList();
            }

            return Json(channels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedClientContracts([DataSourceRequest] DataSourceRequest request, string searchboxDeletedContracts)
        {
            List<ClientContractViewModel> contracts;
            if (searchboxDeletedContracts == "")
            {
                contracts = this.Data.ClientContracts
                   .AllWithDeleted()
                   .Where(c => c.IsDeleted == true)
                   .Select(ClientContractViewModel.FromClientContract)
                   .ToList();
            }
            else
            {
                contracts = this.Data.ClientContracts
                .AllWithDeleted()
                .Where(c => c.TypeOfContract.ToString().Contains(searchboxDeletedContracts) && c.IsDeleted == true)
                .Select(ClientContractViewModel.FromClientContract)
                .ToList();
            }

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedProviderContracts([DataSourceRequest] DataSourceRequest request, string searchboxDeletedContracts)
        {
            List<ProviderContractViewModel> contracts;
            if (searchboxDeletedContracts == "")
            {
                contracts = this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.IsDeleted == true)
                .Select(ProviderContractViewModel.FromProviderContract)
                .ToList();
            }
            else
            {
                contracts = this.Data.ProviderContracts
                .AllWithDeleted()
                .Where(c => c.TypeOfContract.ToString().Contains(searchboxDeletedContracts) && c.IsDeleted == true)
                .Select(ProviderContractViewModel.FromProviderContract)
                .ToList();
            }

            return Json(contracts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ConfirmRestoreClient(int clientId)
        {
            var client = await this.Data.Clients
                .AllWithDeleted()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            client.IsDeleted = false;
            client.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, clientId.ToString(), ActivityTargetType.Client);

            return new EmptyResult();
        }

        public async Task<ActionResult> ConfirmRestoreProvider(int providerId)
        {
            var provider = await this.Data.Providers
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == providerId);

            provider.IsDeleted = false;
            provider.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, providerId.ToString(), ActivityTargetType.Provider);

            return new EmptyResult();
        }

        public async Task<ActionResult> ConfirmRestoreChannel(int channelId)
        {
            var channel = await this.Data.Channels
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == channelId);

            channel.IsDeleted = false;
            channel.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, channelId.ToString(), ActivityTargetType.Channel);

            return new EmptyResult();
        }

        public async Task<ActionResult> ConfirmRestoreClientContract(int contractId)
        {
            var contract = await this.Data.ClientContracts
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == contractId);

            contract.IsDeleted = false;
            contract.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, contractId.ToString(), ActivityTargetType.Contract);

            return new EmptyResult();
        }

        public async Task<ActionResult> ConfirmRestoreProviderContract(int contractId)
        {
            var contract = await this.Data.ProviderContracts
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == contractId);

            contract.IsDeleted = false;
            contract.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, contractId.ToString(), ActivityTargetType.Contract);

            return new EmptyResult();
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