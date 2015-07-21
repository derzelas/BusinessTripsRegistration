using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        private readonly string cookieName = ConfigurationManager.AppSettings["Cookie"];

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

            return View("RegisterMailSent");
        }

        public ActionResult ConfirmRegistration(string guid)
        {
            Guid parsedGuid;
            if (!Guid.TryParse(guid, out parsedGuid))
            {
                ViewBag.ExceptionMessage = "The link is invalid";
                return View("ErrorEncountered");
            }
            var registrationConfirmationModel = new RegistrationConfirmationModel();

            registrationConfirmationModel.Id = parsedGuid;
            registrationConfirmationModel.Confirm();

            return View("ConfirmRegistration");
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
                return View("UnknownUser");
            }

            FormsAuthentication.SetAuthCookie(userModel.Id.ToString(), false);
            return RedirectToAction("GetAllBusinessTrips", "BusinessTrip");
        }

        [Authorize(Roles = "HR,Regular")]
        public ActionResult Logout()
        {
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
            if (filterContext.Exception is UserNotFoundInDataBaseException)
            {
                filterContext.ExceptionHandled = true;

                ViewBag.ExceptionMessage = filterContext.Exception.Message;
                filterContext.Result = View("ErrorEncountered");
            }

            if (filterContext.Exception is InvalidOperationException)
            {
                filterContext.ExceptionHandled = true;

                ViewBag.ExceptionMessage = "There are problem with DataBase; Please try again Later";
                filterContext.Result = View("ErrorEncountered");
            }

            base.OnException(filterContext);
        }
    }
}