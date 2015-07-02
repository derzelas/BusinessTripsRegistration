using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessTrips.Models;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        [HttpPost]
        public ActionResult Register(UserRegistrationModel userRegistrationModel)
        {
           userRegistrationModel.Save();
           userRegistrationModel.SendEmail();
           return View("RegisterMailSent");
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