using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessTrips.Models;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        //
        // GET: /UserOperations/

        [HttpPost]
        public ActionResult RegisterNewUser(UserRegistrationModel model)
        {

            
            return View();
        }

        public ActionResult RegisterNewUser()
        {
            return View();
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