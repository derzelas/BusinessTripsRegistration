using System;
using System.Net;
using System.Net.Mail;

namespace BusinessTrips.Services
{
    public class Email
    {
        private SmtpClient client;
        private string senderAddress;

        public Email()
        {
            senderAddress = "iQuestBusinessTrips@gmail.com";

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderAddress, "Ana@re6mere")
            };
        }

        public void SendConfirmationEmail(string receiverAddress, Guid id)
        {
            var mailMessage = new MailMessage
             {
                 From = new MailAddress(senderAddress),
                 Subject = "E-mail confirmation",
                 Body = GenerateConfirmationMessage(id),
                 To = { receiverAddress }
             };

            client.Send(mailMessage);
        }

        private string GenerateConfirmationMessage(Guid id)
        {
            var welcomeMessage = "Welcome to Business trips. Here is your confirmation link: ";

            string link = String.Format("http://{0}:{1}/UserOperations/ConfirmRegistration/?guid={2}",
                System.Web.HttpContext.Current.Request.Url.Host,
                System.Web.HttpContext.Current.Request.Url.Port,
                id);

            return welcomeMessage + link;
        }
    }
}