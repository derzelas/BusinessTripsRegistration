using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    [Authorize(Roles = "Regular,HR")]
    public class BusinessTripController : Controller
    {
        public ActionResult RegisterBusinessTrip()
        {
            return View("RegisterBusinessTrip");
        }

        [HttpPost]
        public ActionResult RegisterBusinessTrip(BusinessTripModel businessTripModel)
        {
            if (ModelState.IsValid)
            {
                var cookieValue = Request.Cookies["Cookie"].Value;
                string email = FormsAuthentication.Decrypt(cookieValue).Name;

                var repository = new UserRepository();
                var entity = repository.GetByEmail(email);
                businessTripModel.UserId = entity.Id;
                
                businessTripModel.Save();

                Email userEmail = new Email();
                userEmail.SendEmailToBusinessTripOperator(businessTripModel.Id);

                return View("MyBusinessTrips");
            }
            return View("RegisterBusinessTrip");
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

        public ActionResult SearchBusinessTrips()
        {
            return View("OthersBusinessTrips", new BusinessTripCollectionModel());
        }

        [HttpPost]
        public ActionResult SearchBusinessTrips(BusinessTripCollectionModel businessTripCollectionModel)
        {
            businessTripCollectionModel.LoadOtherBusinessTrips();

            return View("OthersBusinessTrips", businessTripCollectionModel);
        }
    }
}