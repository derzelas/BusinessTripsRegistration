using System;
using System.Web.Mvc;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        //
        // GET: /BusinessTrip/
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

            BusinessTripsRepository tripsRepository = new BusinessTripsRepository();
            BusinessTripModel retreivedModel = tripsRepository.GetById(businessTripModel.Id);

            if (retreivedModel != null && retreivedModel.Status == "Pending")
            {
                return View(retreivedModel);
            }

            return View("RequestNotFound");
        }
    }
}