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
    using Services.Data.ViewModels.Contracts.Discussions;
    
    [Authorize]
    public class DiscussionsController : BaseController
    {
        private readonly IDiscussionsServices discussions;

        public DiscussionsController(IDiscussionsServices discussions)
        {
            this.discussions = discussions;
        }

        [HttpGet]
        public ActionResult AllClientsDiscussions(int clientId)
        {
            return PartialView("_ClientDiscussions", clientId);
        }

        [HttpGet]
        public ActionResult AllProvidersDiscussions(int providerId)
        {
            return PartialView("_ProviderDiscussions", providerId);
        }

        [HttpGet]
        public ActionResult DiscussionInformation(int discussionId)
        {
            var discussion = this.discussions.DiscussionInformation(discussionId);

            return PartialView("DiscussionInformation", discussion);
        }

        public ActionResult ClientsDiscussionsNames([DataSourceRequest]DataSourceRequest request)
        {
            var discussionsSubjects = this.discussions.ClientsDiscussionsNames();

            return Json(discussionsSubjects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProvidersDiscussionsNames([DataSourceRequest]DataSourceRequest request)
        {
            var discussionsSubjects = this.discussions.ProvidersDiscussionsNames();

            return Json(discussionsSubjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadClientsDiscussions([DataSourceRequest] DataSourceRequest request, string searchTerm, int clientId, bool? showAll)
        {
            var readDiscussions = (showAll != null) ? (this.discussions.ReadClientsDiscussions(searchTerm, clientId, (bool)showAll)) : (this.discussions.ReadClientsDiscussions(searchTerm, clientId, false));

            return Json(readDiscussions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadProvidersDiscussions([DataSourceRequest] DataSourceRequest request, string searchTerm, int providerId, bool? showAll)
        {
            var readDiscussions = (showAll != null) ? (this.discussions.ReadProvidersDiscussions(searchTerm, providerId, (bool)showAll)) : (this.discussions.ReadProvidersDiscussions(searchTerm, providerId, false));

            return Json(readDiscussions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateDiscussion([DataSourceRequest]  DataSourceRequest request, DiscussionViewModel discussion, int? currentClientId, int? currentProviderId)
        {
            if (discussion == null || !ModelState.IsValid)
            {
                return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var createdDiscussion = this.discussions.CreateDiscussion(discussion, currentClientId, currentProviderId, loggedUserId);

            Base.CreateActivity(ActivityType.Create, createdDiscussion.Id.ToString(), ActivityTargetType.Discussion, loggedUserId);

            discussion.Id = createdDiscussion.Id;

            return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateDiscussion([DataSourceRequest] DataSourceRequest request, DiscussionViewModel discussion)
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

            if (discussion == null || !ModelState.IsValid)
            {
                return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedDiscussion = this.discussions.UpdateDiscussion(discussion);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedDiscussion.Id.ToString(), ActivityTargetType.Discussion, loggedUserId);

            return Json((new[] { discussion }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyDiscussion([DataSourceRequest] DataSourceRequest request, DiscussionViewModel discussion)
        {
            var deletedDiscussion = this.discussions.DestroyDiscussion(discussion);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, discussion.Id.ToString(), ActivityTargetType.Discussion, loggedUserId);

            return Json(new[] { discussion }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult UpcomingDiscussions()
        {
            var upcomingDiscussions = this.discussions.UpcomingDiscussions();

            return View(upcomingDiscussions);
        }
    }
}