namespace TVChannelsCRM.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data;
    using Data.Models;

    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ITVChannelsCRMData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            //List<ActivityViewModel> lastActivities;
            //
            //if (this.User.IsInRole("Admin"))
            //{
            //    lastActivities = await this.Data.Activities
            //        .All()
            //        .Select(ActivityViewModel.FromActivity)
            //        .ToListAsync();
            //}
            //else
            //{
            //    lastActivities = await this.Data.Activities
            //        .All()
            //        .Select(ActivityViewModel.FromActivity)
            //        .Where(a => a.TargetType != ActivityTargetType.User && a.Type != ActivityType.Restore)
            //        .ToListAsync();
            //}

            //return View(lastActivities);

            return RedirectToAction("Index", "Activities", new {area = "Administration"});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            for (int i = 100; i < 200; i++)
            {
                var provider = new Data.Models.Provider()
                {
                    Name = "Name " + (i * 23),
                    Type = TypeOfCompany.OOD,
                    Address = "Al Malinow" + (i * 23),
                    Commission = (i * 23).ToString(),
                    ContactPerson = "Pesho" + (i * 23),
                    Term = "Term" + (i * 23),
                    CPS = "CPS" + (i * 23),
                    Eik = "Eik" + (i * 23),
                    ResidenceAndAddress = " ResidenceAndAddress " + (i * 20),
                    NetworkPage = "NetworkPage" + i * 3.6,
                    PhoneNumber = "0891929384",
                    Email = "Pesho" + i + "@gmail.com"
                };

                this.Data.Providers.Add(provider);
                this.CreateActivity(ActivityType.Create, provider.Id.ToString(), ActivityTargetType.Provider);
            }
            this.Data.SaveChanges();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            for (int i = 100; i < 200; i++)
            {
                var firstClient = new Client()
                {
                    IsActive = true,
                    // ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i).ToString(),
                    // ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i).ToString(),
                    Mg = "MG" + i * 0.4,
                    IrdCard = i * 19.00002 + "IrdCard",
                    Invoicing = i + "asdasd",
                    //  DateOfSigning = DateTime.Now.ToString(),
                    //  DateOfExpiring = DateTime.Now.ToString(),
                    Currency = (i * 20).ToString(),
                    InvoicesIssued = i + "Some Random" + (i / 19.2),
                    PaymentsReceived = i.ToString(),
                    Contract = "Contract" + i
                };

                var secondClient = new Client()
                {
                    IsActive = false,
                    //  ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i).ToString(),
                    // ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i).ToString(),
                    Mg = "MG" + i * 0.4,
                    IrdCard = i * 19.00002 + "IrdCard",
                    Invoicing = i + "asdasd",
                    // DateOfSigning = DateTime.Now.ToString(),
                    // DateOfExpiring = DateTime.Now.ToString(),
                    Currency = (i * 20).ToString(),
                    InvoicesIssued = i + "Some Random" + (i / 19.2),
                    PaymentsReceived = i.ToString(),
                    Contract = "Contract" + i
                };

                this.Data.Clients.Add(firstClient);
                this.Data.Clients.Add(secondClient);
                this.CreateActivity(ActivityType.Create, firstClient.Id.ToString(), ActivityTargetType.Client);
                this.CreateActivity(ActivityType.Create, secondClient.Id.ToString(), ActivityTargetType.Client);
            }
            this.Data.SaveChanges();

            return View();
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