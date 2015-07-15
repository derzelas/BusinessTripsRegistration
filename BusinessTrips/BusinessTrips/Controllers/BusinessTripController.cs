using System;
using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        public ActionResult RegisterBusinessTrip()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterBusinessTrip(BusinessTripModel businessTripModel)
        {
            if (ModelState.IsValid)
            {
                businessTripModel.Save();

                Email email = new Email();
                email.SendEmailToBusinessTripOperator(businessTripModel.Id);

                return View("MyBusinessTrips");
            }
            return View();
        }
        
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

            var tripsRepository = new BusinessTripsRepository();
            var retreivedModel = tripsRepository.GetById(businessTripModel.Id);

            if (retreivedModel != null /*&& retreivedModel.Status == "Pending"*/) 
            {
                return View(retreivedModel);
            }

            return View("RequestNotFound");
        }

        public ActionResult ChangeRequestStatus(Guid id, string status)
        {
            var businessTripModel = new BusinessTripModel {Id = id};

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