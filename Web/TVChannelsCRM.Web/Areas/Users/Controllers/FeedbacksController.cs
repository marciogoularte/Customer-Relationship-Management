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
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            try
            {
                if (string.IsNullOrEmpty(to))
                {
                    to = "n.dermendjievsvc@gmail.com";
                }

                var fromAddress = new MailAddress("NikolayCRMExample@gmail.com", "Nikolay");
                var toFirstAddress = new MailAddress(to);

                const string fromPassword = "123456nd";
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
                    Body = body,
                    IsBodyHtml = true
                };

                //var secondMessage = new MailMessage(fromAddress, toSecondAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //};
                //secondMessage.IsBodyHtml = true;

                try
                {
                    smtp.Send(firstMessage);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Activities");
                }
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
