using System;
using System.Security.Cryptography;
using System.Text;
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
                userRegistrationModel.Password = PasswordEncryption(userRegistrationModel.Id.ToString(),userRegistrationModel.Password);
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

            userModel.Password = PasswordEncryption(userModel.Id.ToString(),userModel.Password);
            if (userModel.Authenthicate())
            {
                return View("AuthenticatedUser");
            }
            return View("UnknownUser");
        }

        public string PasswordEncryption(string salt,string password)
        {

            var hmacMd5 = new HMACMD5(Encoding.UTF8.GetBytes(salt));

            byte[] hashedPassword = hmacMd5.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder encryptedPassword = new StringBuilder();

            for (int i = 0; i < hashedPassword.Length; i++)
            {
                encryptedPassword.Append(hashedPassword[i].ToString(" x2"));
            }
            return encryptedPassword.ToString();
        }
    }
}