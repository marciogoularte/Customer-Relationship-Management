namespace TVChannelsCRM.Web.Areas.Users.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using System.Net.Mail;

    using ViewModels.Feedbacks;

    public class FeedbacksController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeedbackViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(string to, FeedbackViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(to))
                {
                    to = "dermendzhiev.n@gmail.com";
                }

                var fromAddress = new MailAddress("n.dermendjievsvc@gmail.com", "Nikolay");
                var toFirstAddress = new MailAddress(to);
                //var toSecondAddress = new MailAddress("ivaylo.ivanov@virgin.bg");

                const string fromPassword = "1673258nd";
                string subject = string.Format("CRM System Feedback - {0}", model.From);
                string body = string.Format(
                    "From: {0}<br />Title: {1}<br />Table: {2}<br />Table link: {3}<br />Message: {4}",
                    model.From, model.Title, model.Table, model.TableLink, model.Message);

                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                var firstMessage = new MailMessage(fromAddress, toFirstAddress)
                {
                    Subject = subject,
                    Body = body
                };
                firstMessage.IsBodyHtml = true;

                //var secondMessage = new MailMessage(fromAddress, toSecondAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //};
                //secondMessage.IsBodyHtml = true;

                smtp.Send(firstMessage);
                //smtp.Send(secondMessage);

            }
            catch (Exception) 
            { 
            return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}