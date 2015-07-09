using System;
using System.Net;
using System.Net.Mail;

namespace BusinessTrips.Services
{
    public class Email
    {
        private SmtpClient client;
        public const string SenderAddress = "iQuestBusinessTrips@gmail.com";        

        public Email()
        {
            int port = 587;
            client = new SmtpClient("smtp.gmail.com", port)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderAddress, "Ana@re6mere")
            };
        }

        public void SendConfirmationEmail(string receiverAddress, Guid id)
        {
            var mailMessage = new MailMessage
             {
                 From = new MailAddress(SenderAddress),
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