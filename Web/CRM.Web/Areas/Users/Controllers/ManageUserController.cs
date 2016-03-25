namespace CRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Services.Logic.Contracts.Users;
    using Services.Data.ViewModels.Users.ManageUser;

    [Authorize]
    public class ManageUserController : Controller
    {
        private readonly IManageUserServices userServices;

        public ManageUserController(IManageUserServices userServices)
        {
            this.userServices = userServices;
        }
        
        public ActionResult Index(ManageUserViewModel user)
        {
            if (!user.IsNullUser())
            {
                return View(user);
            }

            var loggedUserId = this.User.Identity.GetUserId();
            user = this.userServices.GetUserData(loggedUserId);

            return View(user);
        }

        [HttpPost]
        public ActionResult ManageUserData(ManageUserViewModel model)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Index", new {user = model});
            }

            this.userServices.ManageData(loggedUserId, model);

            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            return RedirectToAction("ChangePassword", "Manage", new {area = ""});
        }
    }
}