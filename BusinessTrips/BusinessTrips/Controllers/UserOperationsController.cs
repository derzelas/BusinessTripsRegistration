using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Model;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        private readonly string cookieName = ConfigurationManager.AppSettings["Cookie"];

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
            Guid parsedGuid;
            if (Guid.TryParse(guid, out parsedGuid))
            {
                var registrationConfirmationModel = new RegistrationConfirmationModel
                {
                    Id = parsedGuid
                };

                registrationConfirmationModel.Confirm();

                return View("ConfirmRegistration");
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
            if (userModel.Authenthicate())
            {
                FormsAuthentication.SetAuthCookie(userModel.Id.ToString(), false);
                return RedirectToAction("GetAllBusinessTrips", "BusinessTrip");
            }
            return View("UnknownUser");
        }

        [Authorize(Roles = "HR,Regular")]
        public ActionResult Logout()
        {
            if (Request.Cookies[cookieName] != null)
            {
                var cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login");
        }
    }
}