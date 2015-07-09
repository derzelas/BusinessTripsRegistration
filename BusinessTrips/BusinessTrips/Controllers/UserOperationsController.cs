using System;
using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        public ActionResult Register()
        {
            return View("Register");
        }

        public ActionResult Logout()
        {
            return View("Logout");
        }

        [HttpPost]
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

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (userModel.Authenthicate())
            {
                return View("AuthenticatedUser");
            }
            return View("UnknownUser");
        }
    }
}