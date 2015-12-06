namespace CRM.Services.Logic.Services.Users
{
    using System.Net;
    using System.Net.Mail;

    using CRM.Data;
    using Contracts.Users;
    using Data.ViewModels.Users.Feedbacks;

    public class FeedbacksServices : IFeedbacksServices
    {
        private ICRMData Data { get; set; }

        public FeedbacksServices(ICRMData data)
        {
            this.Data = data;
        }

        public void SendEmail(string to, FeedbackViewModel model)
        {
            if (string.IsNullOrEmpty(to))
            {
                to = "n.dermendjievsvc@gmail.com";
            }

            var fromAddress = new MailAddress("NikolayCRMExample@gmail.com", "Nikolay");
            var toFirstAddress = new MailAddress(to);

            const string fromPassword = "123456nd";
            var subject = string.Format("CRM System Feedback - {0}", model.From);
            var body = string.Format(
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

            smtp.Send(firstMessage);
        }
    }
}
