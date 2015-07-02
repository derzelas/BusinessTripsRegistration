using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using BusinessTrips.Models;

namespace BusinessTrips.Services
{
    public class Email
    {
        public void Send(UserRegistrationModel user)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            var message = new MailMessage();
            message.From = new MailAddress("iQuestBusinessTrips@gmail.com");
            message.To.Add(user.Email);
            message.Body = "Hello World 2.0";
            message.Subject = "E-mail confirmation";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("iQuestBusinessTrips@gmail.com", "Ana@re6mere");
            client.Send(message);
            message = null;
        }
    }
}