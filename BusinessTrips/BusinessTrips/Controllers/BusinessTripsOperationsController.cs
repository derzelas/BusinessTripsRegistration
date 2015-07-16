using System;
using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.Controllers
{
    public class BusinessTripsOperationsController : Controller
    {
        public ActionResult ManageRequest(string guid)
        {
            var businessTripModel = new BusinessTripModel();
            try
            {
                businessTripModel.Id = Guid.Parse(guid);
            }
            catch (ArgumentNullException)
            {
                return View("RequestNotFound");
            }
            catch (FormatException)
            {
                return View("RequestNotFound");
            }

            try
            {
                var tripsRepository = new BusinessTripsRepository();
                var retreivedModel = tripsRepository.GetById(businessTripModel.Id);

                if (retreivedModel != null)
                {
                    return View(retreivedModel);
                }
            }
            catch (InvalidOperationException)
            {
                return View("RequestNotFound");
            }
            return View("RequestNotFound");
        }

        public ActionResult ChangeRequestStatus(Guid id, string status)
        {
            var businessTripModel = new BusinessTripModel { Id = id };

            businessTripModel.ChangeStatus(status);

            return View("OthersBusinessTrips"); // <-- show all business trips 
            //              ^ move this view to shared folder ??
        }

        public ActionResult RequestDetails(Guid id)
        {
            var tripsRepository = new BusinessTripsRepository();
            var retreivedModel = tripsRepository.GetById(id);
            
            return View(retreivedModel);
        }

    }
}