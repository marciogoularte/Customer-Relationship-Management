namespace TVChannelsCRM.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;

    public abstract class BaseController : Controller
    {
        protected ITVChannelsCRMData Data { get; set; }

        protected BaseController(ITVChannelsCRMData data)
        {
            this.Data = data;
        }

        public void CreateActivity(ActivityType type, string targetId, ActivityTargetType targetType)
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
                    TargetType = targetType,
                    CreatedOn = DateTime.Now
                };

                this.Data.Activities.Add(activity);
            }

            this.Data.SaveChanges();
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
    }
}