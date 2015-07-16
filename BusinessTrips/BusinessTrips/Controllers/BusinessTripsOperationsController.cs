using System;
using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.Controllers
{
    public class BusinessTripsOperationsController : Controller
    {
        public ActionResult GetRequestBy(string guid)
        {
            Guid parsedGuid;

            if (Guid.TryParse(guid, out parsedGuid))
            {
                var tripsRepository = new BusinessTripsRepository();
                var retreivedModel = tripsRepository.GetById(parsedGuid);

                if (retreivedModel != null)
                {
                    return View("ManageRequest", retreivedModel);
                }
            }
            return View("RequestNotFound");
        }

        public ActionResult ChangeRequestStatus(Guid id, string status)
        {
            var businessTripModel = new BusinessTripModel { Id = id };

            businessTripModel.ChangeStatus(status);

            return View("ViewBusinessTrips"); // <-- show all business trips
        }

        public ActionResult RequestDetails(Guid id)
        {
            var tripsRepository = new BusinessTripsRepository();
            var retreivedModel = tripsRepository.GetById(id);

            return View(retreivedModel);
        }

    }
}