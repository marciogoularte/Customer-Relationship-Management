namespace TVChannelsCRM.Web.Areas.Administration.Controllers
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
    using Users.ViewModels.Clients;
    using Users.ViewModels.Providers;

    [Authorize(Roles = "Admin")]
    public class DeletedItemsController : BaseController
    {
        public DeletedItemsController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult DeletedClients()
        {
            return View("DeletedClients");
        }

        public ActionResult DeletedProviders()
        {
            return View("DeletedProviders");
        }

        public JsonResult ReadDeletedClients([DataSourceRequest] DataSourceRequest request)
        {
            var deletedClients = this.Data.Clients
                .AllWithDeleted()
                .Where(c => c.IsDeleted)
                .Select(ClientViewModel.FromClient)
                .ToList();

            return Json(deletedClients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDeletedProviders([DataSourceRequest] DataSourceRequest request)
        {
            var deletedProviders = this.Data.Providers
                .AllWithDeleted()
                .Where(p => p.IsDeleted)
                .Select(ProviderViewModel.FromProvider)
                .ToList();

            return Json(deletedProviders.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ConfirmRestoreClient(int clientId)
        {
            var clientIdToInt = int.Parse(clientId.ToString());
            var client = await this.Data.Clients
                .AllWithDeleted()
                .FirstOrDefaultAsync(c => c.Id == clientIdToInt);

            client.IsDeleted = false;
            client.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, clientId.ToString(), ActivityTargetType.Client);

            return new EmptyResult();
        }

        public async Task<ActionResult> ConfirmRestoreProvider(int providerId)
        {
            var providerIdToInt = int.Parse(providerId.ToString());
            var provider = await this.Data.Providers
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == providerIdToInt);

            provider.IsDeleted = false;
            provider.DeletedOn = null;
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Restore, providerId.ToString(), ActivityTargetType.Provider);

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