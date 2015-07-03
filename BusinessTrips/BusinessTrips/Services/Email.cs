using System;
using System.Net;
using System.Net.Mail;

namespace BusinessTrips.Services
{
    public class Email
    {
        private SmtpClient client;
        private string emailSenderAddress;

        public Email()
        {
            emailSenderAddress = "iQuestBusinessTrips@gmail.com";

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(emailSenderAddress, "Ana@re6mere")
            };
        }

        public void SendConfirmatioEmail(string emailReceiver, string message)
        {
            var mailMessage = new MailMessage
             {
                 From = new MailAddress(emailSenderAddress),
                 Subject = "E-mail confirmation",
                 Body = message,
                 To = { emailReceiver }
             };
            client.Send(mailMessage);
        }
    }
}