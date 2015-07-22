using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [HttpPost]
        public ActionResult Register(UserRegistrationModel userRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                userRegistrationModel.Save();

                var email = new Email();
                email.SendUserRegistrationEmail(userRegistrationModel.Id, userRegistrationModel.Email);

                return View("RegisterMailSent");
            }
            return View("Register");
        }

        public ActionResult ConfirmRegistration(string guid)
        {
            var registrationConfirmationModel = new RegistrationConfirmationModel();

            Guid parsedGuid;
            if (Guid.TryParse(guid, out parsedGuid))
            {
                registrationConfirmationModel.Id = parsedGuid;
                registrationConfirmationModel.Confirm();

                return View("ConfirmRegistration");
            }
            return View("Error");
        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "BusinessTrip");
            }
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (userModel.Authenthicate())
            {
                FormsAuthentication.SetAuthCookie(userModel.Id.ToString(), false);
                return RedirectToAction("Register", "BusinessTrip");
            }
            return View("UnknownUser");
        }

        [Authorize(Roles = "HR,Regular")]
        public ActionResult Logout()
        {
            if (Request.Cookies["Cookie"] != null)
            {
                HttpCookie cookie = new HttpCookie("Cookie");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.ToForgotPasswordModelByEmail(userModel.Email);
                var email = new Email();
                email.SendForgotPasswordEmail(userModel.Id, userModel.Email);

                return View("ForgotPasswordEmailSent");
            }
            return View("ForgotPassword");
        }

        public ActionResult SetNewPassword(string guid)
        {
            return View("SetNewPassword");
        }

        [HttpPost]
        public ActionResult SetNewPassword(SetNewPasswordModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.SetPassword(userModel.Id);

                return View("PasswordSet");
            }

            return View("SetNewPassword");
        }
    }
}