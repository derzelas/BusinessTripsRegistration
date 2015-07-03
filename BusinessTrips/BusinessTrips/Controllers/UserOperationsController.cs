using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessTrips.Models;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        [HttpPost]
        public ActionResult Register(UserRegistrationModel userRegistrationModel)
        {
            userRegistrationModel.Save();
            var message = "http..." + userRegistrationModel.RequestToken;
            Email email = new Email(userRegistrationModel);

            email.Send();

            if (email.IsSent)
            {
                return View("RegisterMailSent");
            }
            return View("RegisterEmailNotSent");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserModel model)
        {
            return View("UnknownUser");
        }
    }
}