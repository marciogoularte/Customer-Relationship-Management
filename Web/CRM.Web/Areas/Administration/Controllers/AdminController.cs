namespace CRM.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Web.Controllers;
    using Services.Logic.Contracts.Administration;
    using Services.Data.ViewModels.Administration.Admin;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminServices admin;

        public AdminController(IAdminServices admin)
        {
            this.admin = admin;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var usernames = this.admin.Index(loggedUserId);

            return View(usernames);
        }

        public JsonResult ReadUsers([DataSourceRequest] DataSourceRequest request, string searchboxUsers)
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var users = this.admin.ReadUsers(searchboxUsers, loggedUserId);

            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState["CreatedOn"].Errors.Clear();
            }

            if (user == null || !ModelState.IsValid)
            {
                return Json(new[] { user }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdUser = this.admin.CreateUser(user);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdUser.Id, ActivityTargetType.User, loggedUserId);

            return Json(new[] { createdUser }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState["CreatedOn"].Errors.Clear();
            }

            if (user == null || !ModelState.IsValid)
            {
                return Json((new[] { user }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
            }

            var updatedUser = this.admin.UpdateUser(user);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedUser.Id, ActivityTargetType.User, loggedUserId);

            return Json((new[] { updatedUser }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyUser([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            var deletedUser = this.admin.DestroyUser(user);

            var loggedUserId = this.User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedUser.Id.ToString(), ActivityTargetType.User, loggedUserId);

            return Json(new[] { deletedUser }, JsonRequestBehavior.AllowGet);
        }
    }
}