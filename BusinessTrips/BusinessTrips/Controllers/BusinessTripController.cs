using System.Web.Mvc;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        //
        // GET: /BusinessTrip/
        public ActionResult Index()
        {
            return View("RegisterBusinessTrip");
        }
	}
}