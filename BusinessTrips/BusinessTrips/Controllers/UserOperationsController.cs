using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessTrips.Models;
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        //
        // GET: /UserOperations/

        [HttpPost]
        public void RegisterNewUser(UserRegistrationModel userCredantials)
        {
            IStorage<UserRegistrationModel> storage = new InMemoryStorage<UserRegistrationModel>();

            UserRegistrationRepository registrationRepository = new UserRegistrationRepository(storage);
            registrationRepository.Add(userCredantials);

        }

        public ActionResult RegisterNewUser()
        {
            return View("");
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