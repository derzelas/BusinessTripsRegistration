using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessTrips.Controllers
{
    public class UserOperationsController : Controller
    {
        //
        // GET: /UserOperations/

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