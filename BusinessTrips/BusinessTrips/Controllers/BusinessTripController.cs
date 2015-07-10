using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        //
        // GET: /BusinessTrip/
        public ActionResult AddBusinessTrip()
        {
            return View("RegisterBusinessTrip");
        }

        [HttpPost]
        public ActionResult AddBusinessTrip(BusinessTripModel businessTripModel)
        {
            var businessTripRepository = new BusinessTripsRepository();
            businessTripRepository.Add(businessTripModel);
            businessTripRepository.CommitChanges();

            return View("RegisterBusinessTrip");
        }
	}
}