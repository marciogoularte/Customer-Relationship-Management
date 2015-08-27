using System.Net;
using System.Net.Mail;

namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    public class EmailingController : Controller
    {
        // GET: Users/Emailing
        public ActionResult Index()
        {
            return new EmptyResult();
            // return View();
        }

        public ActionResult SendEmail(string to)
        {
            if (string.IsNullOrEmpty(to))
            {
                to = "dermendzhiev.n@gmail.com";
            }

            var fromAddress = new MailAddress("dermendzhiev.n@gmail.com", "Nikolay");
            var toAddress = new MailAddress(to);

            const string fromPassword = "1673258nd";
            const string subject = "Sample email subject";
            const string body = "Body";

            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl= true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            smtp.Send(message);

            return Content("Message is successfully send!");
        }
    }
}