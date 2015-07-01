using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
      
        [HttpPost]
        public ActionResult RegisterNewUSer(UserRegistrationModel userCredentials)
        {
            IStorage<UserRegistrationModel> storage = new InMemoryStorage<UserRegistrationModel>();

            UserRegistrationRepository registrationRepository = new UserRegistrationRepository(storage);
            registrationRepository.Add(userCredentials);

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