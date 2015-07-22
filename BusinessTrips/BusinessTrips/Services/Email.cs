using System;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BusinessTrips.Services
{
    public class Email
    {
        private const int Port = 587;
        private const string SenderAddress = "iQuestBusinessTrips@gmail.com";
        private const string BusinessTripOperatorAddress = "iQuestBusinessTrips@gmail.com";
        private const string Password = "Ana@re6mere";
        private const string SmtpClient = "smtp.gmail.com";

        private readonly SmtpClient client;

        public Email()
        {
            client = new SmtpClient(SmtpClient, Port)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderAddress, Password)
            };
        }

        public void SendForgotPasswordEmail(Guid userId, string userEmail)
        {
            const string subject = "Link to change  password";
            string message = "Here is your link to change your password: ";

            message += string.Format("http://{0}:{1}/UserOperations/SetNewPassword/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                userId);

            Send(subject, message, userEmail);
        }

        public void SendUserRegistrationEmail(Guid userId, string userEmail)
        {
            const string subject = "Confirm your email for Business Trips";
            string message = "Welcome to Business trips. Here is your confirmation link: ";

            message += string.Format("http://{0}:{1}/UserOperations/ConfirmRegistration/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                userId);

            Send(subject, message, userEmail);
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
    }
}