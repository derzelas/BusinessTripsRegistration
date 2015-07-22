using System;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BusinessTrips.Services
{
    public class Email
    {
        private SmtpClient Client;
        private const string SenderAddress = "iQuestBusinessTrips@gmail.com";
        private const string BusinessTripOperatorAddress = "iQuestBusinessTrips@gmail.com";
        private const string Password = "Ana@re6mere";
        private const string SmtpClient = "smtp.gmail.com";

        public Email()
        {
            const int port = 587;
            Client = new SmtpClient(SmtpClient, port)
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

            Client.Send(message);
        }

        public void SendForgotPasswordEmail(Guid id, string receiverEmail)
        {
            const string subject = "Link to change  password";
            const string welcomeMessage = "Here is your link to change your password: ";

            var link = String.Format("http://{0}:{1}/UserOperations/SetNewPassword/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            Send(subject, welcomeMessage + link, receiverEmail);
        }

        public void SendUserRegistrationEmail(Guid id, string receiverEmail)
        {
            const string subject = "Confirm your email for Business Trips";
            const string welcomeMessage = "Welcome to Business trips. Here is your confirmation link: ";

            var link = String.Format("http://{0}:{1}/UserOperations/ConfirmRegistration/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            Send(subject, welcomeMessage + link, receiverEmail);
        }

        public void SendBusinessTripRegistrationEmail(Guid id)
        {
            const string subject = "New Business Trip request pending";
            const string message = "A new business trip has been registered, to accept/reject the request click here: ";

            var link = GetLinkToBusinessTrip(id);

            Send(subject, message + link, BusinessTripOperatorAddress);
        }

        public void SendCancelBusinessTripEmail(Guid id)
        {
            const string subject = "Request canceled";
            const string message = "The following business trip has been canceled: ";

            var link = GetLinkToBusinessTrip(id);

            Send(subject, message + link, BusinessTripOperatorAddress);
        }

        private static string GetLinkToBusinessTrip(Guid id)
        {
            var link = string.Format("http://{0}:{1}/BusinessTripsOperations/GetRequestBy/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);
            return link;
        }
    }
}