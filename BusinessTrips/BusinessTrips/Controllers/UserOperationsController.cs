using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.Services;
using Roles = BusinessTrips.DAL.Storage.Roles;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        [HttpPost]
        public ActionResult ForgotPasswordActionResult(ForgotPasswordModel userForgotPasswordModelModel)
        {
            if (ModelState.IsValid)
            {
                var email = new Email();
                email.SendUserRegistrationEmail(userForgotPasswordModelModel.Id, userForgotPasswordModelModel.Email);

                return View("SetNewPassword");
            }
            return View("ForgotPassword");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationModel userRegistrationModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            userRegistrationModel.Save();

            var email = new Email();
            email.SendUserRegistrationEmail(userRegistrationModel.Id, userRegistrationModel.Email);

            return View("RegistrationSuccessful");
        }

        public ActionResult ConfirmRegistration(string guid)
        {
            Guid parsedGuid;
            if (Guid.TryParse(guid, out parsedGuid))
            {
                var registrationConfirmationModel = new RegistrationConfirmationModel();

                registrationConfirmationModel.Id = parsedGuid;
                registrationConfirmationModel.Confirm();

                return View("RegistrationConfirmationSuccessful");
            }
            return View("ErrorEncountered");
        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("GetUserBusinessTrips", "BusinessTrip");
            }
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (!userModel.Authenthicate())
            {
                return View("InvalidUser");
            }

            FormsAuthentication.SetAuthCookie(userModel.Id.ToString(), false);
            return RedirectToAction("GetUserBusinessTrips", "BusinessTrip");
        }

        [RoleAuthorize(Roles.Regular, Roles.Hr)]
        public ActionResult Logout()
        {
            string cookieName = ConfigurationManager.AppSettings["Cookie"];

            if (Request.Cookies[cookieName] == null)
            {
                return RedirectToAction("Login");
            }

            var cookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1d)
            };

            Response.Cookies.Add(cookie);

            return RedirectToAction("Login");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is UserNotFoundException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("ErrorEncountered");
            }

            base.OnException(filterContext);
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel userForgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                userForgotPasswordModel.ToForgotPasswordModelByEmail(userForgotPasswordModel.Email);
                var email = new Email();
                email.SendForgotPasswordEmail(userForgotPasswordModel.Id, userForgotPasswordModel.Email);

                return View("ForgotPasswordEmailSent");
            }
            return View("ForgotPassword");
        }

        public ActionResult SetNewPassword(string guid)
        {
            var userSetNewPasswordModel = new SetNewPasswordModel() { Id = Guid.Parse(guid) };
            return View("SetNewPassword", userSetNewPasswordModel);
        }

        [HttpPost]
        public ActionResult SetNewPassword(SetNewPasswordModel userSetNewPasswordModel)
        {
            if (ModelState.IsValid)
            {
                userSetNewPasswordModel.SetPassword();

                return View("PasswordSet");
            }
            return View("SetNewPassword");
        }
    }
}