using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    [Authorize(Roles = "Regular")]
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
                var entity = GetUserEntityFromSession();

                businessTripModel.User = entity;

                businessTripModel.Save();

                Email userEmail = new Email();
                userEmail.SendEmailToBusinessTripOperator(businessTripModel.Id);

                return View("BusinessTripAdded");
            }

            return View("RegisterBusinessTrip");
        }

        public ActionResult RequestDetails(Guid id)
        {
           var tripsRepository = new BusinessTripsRepository();
            var retreivedModel = tripsRepository.GetById(id);
            
            return View(retreivedModel);
        }
        
        public ActionResult ViewMyBusinessTrips()
        {
            var entity = GetUserEntityFromSession();

            var businessTripCollectionModel = new BusinessTripCollectionModel();
            businessTripCollectionModel.LoadBusinessTripForUser(entity.Id);

            return View("MyBusinessTrips", businessTripCollectionModel);
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

        private UserEntity GetUserEntityFromSession()
        {
            var cookieValue = Request.Cookies["Cookie"].Value;
            string email = FormsAuthentication.Decrypt(cookieValue).Name;

            var repository = new UserRepository();
            var entity = repository.GetByEmail(email);

            return entity;
        }
    }
}