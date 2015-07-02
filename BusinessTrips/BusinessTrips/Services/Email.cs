using System;
using System.Net;
using System.Net.Mail;
using BusinessTrips.Models;

namespace BusinessTrips.Services
{
    public class Email
    {
        public bool IsSent { get; private set; }
        private SmtpClient client;
        private MailMessage message;

        public Email(UserRegistrationModel user)
        {
            string emailSenderAddress = "iQuestBusinessTrips@gmail.com";

            message = new MailMessage
            {
                From = new MailAddress(emailSenderAddress),
                Subject = "E-mail confirmation",
                Body = BodyConfiguration(),
                To = { user.Email }
            };

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(emailSenderAddress, "Ana@re6mere")
            };
        }

        private string BodyConfiguration()
        {
            Uri uri = new Uri("http://msdn.com");

            string bodyMessage = "Welcome message ";
            bodyMessage += uri.AbsoluteUri;

            return bodyMessage;
        }
        public void Send()
        {
            try
            {
                client.Send(message);
                IsSent = true;
            }
            catch (SmtpException e)
            {
                IsSent = false;
            }
        }
    }
}