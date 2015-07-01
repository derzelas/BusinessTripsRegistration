using System.Web.Mvc;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        [HttpPost]
        public ActionResult RegisterNewUSer(UserRegistrationModel userRegistrationModel)
        {
            IStorage<UserRegistrationModel> storage = new InMemoryStorage<UserRegistrationModel>();

            UserRegistrationRepository registrationRepository = new UserRegistrationRepository(storage);
            registrationRepository.Add(userRegistrationModel);

            return View("RegisterMailSent");
        }

        public ActionResult RegisterNewUser()
        {
            return View("Register");
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult AuthenticateUser()
        {
            return View();
        }
	}
}