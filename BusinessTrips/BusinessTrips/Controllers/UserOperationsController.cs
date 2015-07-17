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

        // Should I test it ?
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("RegisterBusinessTrip", "BusinessTrip");
            }

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (userModel.Authenthicate())
            {
                FormsAuthentication.SetAuthCookie(userModel.Email, false);
                return RedirectToAction("RegisterBusinessTrip", "BusinessTrip");
            }
            return View("UnknownUser");
        }

        // Should I test it ?
        [Authorize(Roles = "HR,Regular")]
        public ActionResult Logout()
        {
            if (Request.Cookies["Cookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("Cookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);

                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Login");
        }
    }
}