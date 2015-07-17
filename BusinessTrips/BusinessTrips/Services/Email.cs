using System;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BusinessTrips.Services
{
    public abstract class EmailBase
    {
        protected SmtpClient Client;
        protected const string SenderAddress = "iQuestBusinessTrips@gmail.com";
        protected const string BusinessTripOperatorAddress = "iQuestBusinessTrips@gmail.com";


        protected EmailBase()
        {
            const int port = 587;
            Client = new SmtpClient("smtp.gmail.com", port)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderAddress, "Ana@re6mere")
            };
        }

        public abstract void Send(Guid id, string receiverAddress = "iQuestBusinessTrips@gmail.com");
    }

    public class UserRegistrationEmail : EmailBase
    {
        public override void Send(Guid id, string receiverAddress)
        {
            var message = new MailMessage
            {
                From = new MailAddress(SenderAddress),
                Subject = "E-mail confirmation",
                Body = GenerateBodyMessage(id),
                To = { receiverAddress }
            };

            Client.Send(message);
        }

        private static string GenerateBodyMessage(Guid id)
        {
            const string welcomeMessage = "Welcome to Business trips. Here is your confirmation link: ";

            var link = string.Format("http://{0}:{1}/UserOperations/ConfirmRegistration/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            return welcomeMessage + link;
        }
    }

    public class BusinessTripRegistrationEmail : EmailBase
    {
        public override void Send(Guid id,)
        {
            var message = new MailMessage
           {
               From = new MailAddress(SenderAddress),
               Subject = "New request pending",
               Body = GenerateBodyMessage(id),
               To = { BusinessTripOperatorAddress }
           };

            Client.Send(message);
        }

        private static string GenerateBodyMessage(Guid id)
        {
            const string message = "A new business trip has been registered, to accept/reject the request click here: ";

            var link = string.Format("http://{0}:{1}/BusinessTripsOperations/GetRequestBy/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            return message + link;
        }
    }

    public class BusinessTripCancellationEmail : EmailBase
    {
        public override void Send(Guid id, string receiverAddress)
        {
            var message = new MailMessage
            {
                From = new MailAddress(SenderAddress),
                Subject = "Request canceled",
                Body = GenerateBodyMessage(id),
                To = { BusinessTripOperatorAddress }
            };

            Client.Send(message);
        }

        private static string GenerateBodyMessage(Guid id)
        {
            const string message = "The following business trip has been canceled: ";

            var link = string.Format("http://{0}:{1}/BusinessTripsOperations/GetRequestBy/?guid={2}",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                id);

            return message + link;
        }
    }
}