using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;
using System.Net;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        //
        // GET: /UserOperations/

        public class Email
        {
            public Email()
            {
            SmtpClient  smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl=true;
            smtp.UseDefaultCredentials=false;

            smtp.Credentials = new NetworkCredential("user", "parola");
            MailMessage mail = new MailMessage("sandica_robert@gmail.com", "petrica.bota2@yahoo.com", "Message", "Hello World");
            smtp.Send(mail);

            }
            
        }
        public ActionResult RegisterNewUser()
        {
            //save in depository
            //send e mail
            
            return View();
        }
        
        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult AuthenticateUser()
        {
            return View();
        }

        /*
         int l=0,O=1;
         
         if(l==0)
            O=1;
         else
            O=0;
         
         */ 
	}
}