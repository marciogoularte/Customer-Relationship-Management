namespace TVChannelsCRM.Web.Controllers
{
    using System.Web.Mvc;

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

            return RedirectToAction("Index", "Activities", new { area = "Administration" });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            for (int i = 100; i < 200; i++)
            {
                var provider = new Data.Models.Provider()
                {
                    Name = "Name " + (i * 23),
                    //Type = TypeOfCompany.OOD,
                    Address = "Al Malinow" + (i * 23),
                    Commission = (i * 23).ToString(),
                    ContactPerson = "Pesho" + (i * 23),
                    Term = "Term" + (i * 23),
                    Cps = i * 23,
                    Uic = "Uic" + (i * 23),
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
                    //IsActive = true,
                    //ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i).ToString(),
                    //ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i).ToString(),
                    //Mg = "MG" + i * 0.4,
                    //IrdCard = i * 19.00002 + "IrdCard",
                    //Invoicing = i + "asdasd",
                    //DateOfSigning = DateTime.Now.ToString(),
                    //DateOfExpiring = DateTime.Now.ToString(),
                    //Currency = (i * 20).ToString(),
                    //InvoicesIssued = i + "Some Random" + (i / 19.2),
                    //PaymentsReceived = i.ToString(),
                    //Contract = "Contract" + i
                };

                var secondClient = new Client()
                {
                    //IsActive = false,
                    //ActiveFrom = DateTime.Now.AddHours(i).AddMinutes(-i).ToString(),
                    //ActiveTo = DateTime.Now.AddHours(-i).AddMinutes(i).ToString(),
                    //Mg = "MG" + i * 0.4,
                    //IrdCard = i * 19.00002 + "IrdCard",
                    //Invoicing = i + "asdasd",
                    //DateOfSigning = DateTime.Now.ToString(),
                    //DateOfExpiring = DateTime.Now.ToString(),
                    //Currency = (i * 20).ToString(),
                    //InvoicesIssued = i + "Some Random" + (i / 19.2),
                    //PaymentsReceived = i.ToString(),
                    //Contract = "Contract" + i
                };

                this.Data.Clients.Add(firstClient);
                this.Data.Clients.Add(secondClient);
                this.CreateActivity(ActivityType.Create, firstClient.Id.ToString(), ActivityTargetType.Client);
                this.CreateActivity(ActivityType.Create, secondClient.Id.ToString(), ActivityTargetType.Client);
            }
            this.Data.SaveChanges();

            return View();
        }

        public ActionResult GeneratePdf()
        {
            return new Rotativa.ViewAsPdf("SampleContract") { FileName = "TestViewAsPdf.pdf" };
        }
    }
}