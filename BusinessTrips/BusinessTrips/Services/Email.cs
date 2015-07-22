using System;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BusinessTrips.Services
{
    public class Email
    {
        private const int Port = 587;
        private readonly SmtpClient client;
        private const string SenderAddress = "iQuestBusinessTrips@gmail.com";
        private const string BusinessTripOperatorAddress = "iQuestBusinessTrips@gmail.com";
        private const string Password = "Ana@re6mere";
        private const string SmtpClient = "smtp.gmail.com";

        public Email()
        {
            client = new SmtpClient(SmtpClient, Port)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderAddress, Password)
            };
        }

        private void Send(string subject, string body, string receiver)
        {
            var message = new MailMessage
            {
                From = new MailAddress(SenderAddress),
                Subject = subject,
                Body = body,
                To = { receiver }
            };

            client.Send(message);
        }

        public void SendUserRegistrationEmail(Guid userId, string reveiverEmail)
        {
            const string subject = "Confirm your email for Business Trips";
            const string welcomeMessage = "Welcome to Business trips. Here is your confirmation link: ";

            var link = String.Format("http://{0}:{1}/UserOperations/ConfirmRegistration/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                userId);

            Send(subject, welcomeMessage + link, reveiverEmail);
        }

        public void SendBusinessTripRegistrationEmail(Guid businessTripId)
        {
            const string subject = "New Business Trip request pending";
            string message = "A new business trip has been registered, to accept/reject the request click here: ";
            message += GetLinkToBusinessTripBy(businessTripId);

            Send(subject, message, BusinessTripOperatorAddress);
        }

        public void SendCancelBusinessTripEmail(Guid businessTripId)
        {
            const string subject = "Request canceled";
            string message = "The following business trip has been canceled: ";
            message += GetLinkToBusinessTripBy(businessTripId);

            Send(subject, message, BusinessTripOperatorAddress);
        }

        private static string GetLinkToBusinessTripBy(Guid businessTripId)
        {
            var link = string.Format("http://{0}:{1}/BusinessTrip/GetBy/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                businessTripId);

            return link;
        }
    }
}