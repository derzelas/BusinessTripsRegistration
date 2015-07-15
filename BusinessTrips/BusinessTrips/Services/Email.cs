using System;
using System.Net;
using System.Net.Mail;
using System.Web;

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
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            return welcomeMessage + link;
        }

        public void SendEmailToBusinessTripOperator(Guid id)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(SenderAddress),
                Subject = "New request pending",
                Body = GenerateBusinessTripRegistrationMessage(id),
                To = { SenderAddress }
            };

            client.Send(mailMessage);
        }

        private string GenerateBusinessTripRegistrationMessage(Guid id)
        {
            var message = "A new business trip has been registered, to accept/reject the request click here: ";

            string link = String.Format("http://{0}:{1}/BusinessTrip/ManageRequest/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            return message + link;
        }
    }
}