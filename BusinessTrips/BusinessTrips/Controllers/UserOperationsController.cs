using System;
using System.Web.Mvc;
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

        public ActionResult Logout()
        {
            //controler creat pentru logoff
            return View("Logout");
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
            if (userModel.Authenthicate())
            {
                return View("AuthenticatedUser");
            }
            return View("UnknownUser");
        }


    }
}