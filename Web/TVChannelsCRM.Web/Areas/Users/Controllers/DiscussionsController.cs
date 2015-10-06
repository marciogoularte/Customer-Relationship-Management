namespace TVChannelsCRM.Web.Areas.Users.Controllers
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
    using ViewModels.Discussions;

    public class DiscussionsController : BaseController
    {
        public DiscussionsController(ITVChannelsCRMData data)
            : base(data)
        {
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
        public async Task<ActionResult> DiscussionInformation(int discussionId)
        {
            var discussion = await this.Data.Discussions
                .All()
                .Select(DiscussionViewModel.FromDiscussion)
                .FirstOrDefaultAsync(d => d.Id == discussionId);

            return PartialView("DiscussionInformation", discussion);
        }

        public ActionResult ClientsDiscussionsNames([DataSourceRequest]DataSourceRequest request)
        {
            var discussionsSubjects = this.Data.Discussions
                .All()
                .Where(d => d.Client != null)
                .Select(d => d.SubjectOfDiscussion)
                .ToList();

            return Json(discussionsSubjects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProvidersDiscussionsNames([DataSourceRequest]DataSourceRequest request)
        {
            var discussionsSubjects = this.Data.Discussions
                .All()
                .Where(d => d.Provider != null)
                .Select(d => d.SubjectOfDiscussion)
                .ToList();

            return Json(discussionsSubjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadClientsDiscussions([DataSourceRequest] DataSourceRequest request, string searchTerm, int clientId)
        {
            List<DiscussionViewModel> discussions;
            if (string.IsNullOrEmpty(searchTerm) || searchTerm == "")
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ClientId != null &&
                        d.ClientId == clientId)
                    .ToList();
            }
            else
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ClientId != null &&
                        d.ClientId == clientId &&
                        d.SubjectOfDiscussion.Contains(searchTerm))
                    .ToList();
            }

            return Json(discussions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadProvidersDiscussions([DataSourceRequest] DataSourceRequest request, string searchTerm, int providerId)
        {
            List<DiscussionViewModel> discussions;
            if (searchTerm == null || string.IsNullOrEmpty(searchTerm) || searchTerm == "")
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ProviderId != null &&
                        d.ProviderId == providerId)
                    .ToList();
            }
            else
            {
                discussions = this.Data.Discussions
                    .All()
                    .Select(DiscussionViewModel.FromDiscussion)
                    .Where(d =>
                        d.ProviderId != null &&
                        d.ProviderId == providerId &&
                        d.SubjectOfDiscussion.Contains(searchTerm))
                    .ToList();
            }

            return Json(discussions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateDiscussion([DataSourceRequest]  DataSourceRequest request, DiscussionViewModel discussion, int? currentClientId, int? currentProviderId)
        {
            if (discussion == null || !ModelState.IsValid)
            {
                return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            var loggedUserId = this.User.Identity.GetUserId();

            var newDiscussion = new Discussion
            {
                Date = discussion.Date,
                SubjectOfDiscussion = discussion.SubjectOfDiscussion,
                Summary = discussion.Summary,
                UserId = loggedUserId,
                Comments = discussion.Comments
            };

            if (currentClientId != null)
            {
                newDiscussion.ClientId = currentClientId;
                newDiscussion.ProviderId = null;
                newDiscussion.Provider = null;
            }
            else if (currentProviderId != null)
            {
                newDiscussion.ProviderId = currentProviderId;
                newDiscussion.ClientId = null;
                newDiscussion.Client = null;
            }

            this.Data.Discussions.Add(newDiscussion);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Create, newDiscussion.Id.ToString(), ActivityTargetType.Discussion);
            discussion.Id = newDiscussion.Id;

            return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateDiscussion([DataSourceRequest] DataSourceRequest request, DiscussionViewModel discussion)
        {
            var discussionFromDb = this.Data.Discussions
                .All()
                .FirstOrDefault(d => d.Id == discussion.Id);

            foreach (var modelError in ModelState)
            {
                var propertyName = modelError.Key;

                if (propertyName.Contains("Client"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
                else if (propertyName.Contains("Provider"))
                {
                    ModelState[propertyName].Errors.Clear();
                }
            }

            if (discussion == null || !ModelState.IsValid || discussionFromDb == null)
            {
                return Json(new[] { discussion }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            discussionFromDb.Date = discussion.Date;
            discussionFromDb.SubjectOfDiscussion = discussion.SubjectOfDiscussion;
            discussionFromDb.Summary = discussion.Summary;
            discussionFromDb.Comments = discussion.Comments;

            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Edit, discussionFromDb.Id.ToString(), ActivityTargetType.Discussion);

            return Json((new[] { discussion }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyDiscussion([DataSourceRequest] DataSourceRequest request, DiscussionViewModel discussion)
        {
            this.Data.Discussions.Delete(discussion.Id);
            this.Data.SaveChanges();

            this.CreateActivity(ActivityType.Delete, discussion.Id.ToString(), ActivityTargetType.Discussion);

            return Json(new[] { discussion }, JsonRequestBehavior.AllowGet);
        }
    }
}