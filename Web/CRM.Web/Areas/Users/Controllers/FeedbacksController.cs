namespace CRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Web.Mvc;

    using Services.Logic.Contracts.Users;
    using Services.Data.ViewModels.Users.Feedbacks;

    [Authorize]
    public class FeedbacksController : Controller
    {
        private readonly IFeedbacksServices feedbacks;

        public FeedbacksController(IFeedbacksServices feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        public ActionResult Index(FeedbackViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(string to, FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                this.feedbacks.SendEmail(to, model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}