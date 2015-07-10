using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Model;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    [Authorize]
    public class UserOperationsController : Controller
    {
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserRegistrationModel userRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                userRegistrationModel.Save();

                Email email = new Email();
                email.SendConfirmationEmail(userRegistrationModel.Email, userRegistrationModel.Id);
                
                return View("RegisterMailSent");
            }
            return View("Register");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            string userName = userModel.Name;
            string password = userModel.Password;

            bool authenticated = false;

            if (userModel.Authenthicate())
            {
                    authenticated = true;
                // error checking does happen here.

                if (authenticated)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, String.Empty, FormsAuthentication.FormsCookiePath);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);
                    //FormsAuthentication.RedirectFromLoginPage(userName, false);

                    Response.Redirect("~/BusinessTrip/RegisterBusinessTrip");
                }
            }
            return View("UnknownUser");
        }

        public ActionResult ConfirmRegistration(string guid)
        {
            var registrationConfirmationModel = new RegistrationConfirmationModel();
            try
            {
                registrationConfirmationModel.Id = Guid.Parse(guid);
            }
            catch (ArgumentNullException)
            {
                return View("Error");
            }
            catch (FormatException)
            {
                return View("Error");
            }
            
            registrationConfirmationModel.Confirm();

            return View("ConfirmRegistration");
        }

        public ActionResult Not()
        {
            return View("AuthenticatedUser");
        }
    }
}