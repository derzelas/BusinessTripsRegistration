using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    [Authorize(Roles = "Regular,HR")]
    public class BusinessTripController : Controller
    {
        private const string CookieName = "Cookie";

        public ActionResult RegisterBusinessTrip()
        {
            return View("RegisterBusinessTrip");
        }

        [HttpPost]
        public ActionResult RegisterBusinessTrip(BusinessTripModel businessTripModel)
        {
            if (ModelState.IsValid)
            {
                var userEntity = GetUserEntityByEmail(GetUserEmailFromCookie());

                businessTripModel.User = userEntity;
                
                businessTripModel.Save();

                Email userEmail = new Email();
                userEmail.SendEmailToBusinessTripOperator(businessTripModel.Id);

                return View("BusinessTripAdded");
            }
            return View("RegisterBusinessTrip");
        }
        
        private UserEntity GetUserEntityByEmail(string email)
        {
            var repository = new UserRepository();

            return repository.GetByEmail(email);
        }

        // There will always be a cookie because of Authorize, so no check for null is required
        private string GetUserEmailFromCookie()
        {
            var cookieValue = Request.Cookies[CookieName].Value;

            return FormsAuthentication.Decrypt(cookieValue).Name;
        }

        public ActionResult ViewMyBusinessTrips()
        {
            var entity = GetUserEntityByEmail(GetUserEmailFromCookie());

            var myBusinessTripsCollection = new MyBusinesTripsCollectionViewModel
            {
                MyBusinesTripsViewModels = entity.BusinessTrips.Select(e => e.ToMyViewModel())
            };

            return View("MyBusinessTrips", myBusinessTripsCollection);
        }

        public ActionResult CancelRequest(Guid id)
        {
            var entity = GetUserEntityByEmail(GetUserEmailFromCookie());

            var myBusinessTripsCollection = new MyBusinesTripsCollectionViewModel
            {
                MyBusinesTripsViewModels = entity.BusinessTrips.Select(e => e.ToMyViewModel())
            };

            if (entity.BusinessTrips.Single(b => b.Id == id).Status == RequestStatus.Accepted)
            {
                Email userEmail = new Email();
                userEmail.SendEmailToBusinessTripOperator(entity.BusinessTrips.Single(b => b.Id == id).Id);
            }

            entity.BusinessTrips.Single(b => b.Id == id).Status = RequestStatus.Canceled;

            return View("MyBusinessTrips", myBusinessTripsCollection);
        }
        
        public ActionResult RequestDetails(Guid id)
        {
            var tripsRepository = new BusinessTripsRepository();
            var retreivedModel = tripsRepository.GetById(id);

            return View("RequestDetails", retreivedModel);
        }

        public ActionResult SearchBusinessTrips()
        {
            return View("OthersBusinessTrips", new OtherBusinessTripsCollectionViewModel());
        }

        [HttpPost]
        public ActionResult SearchBusinessTrips(OtherBusinessTripsCollectionViewModel businessTripsCollectionViewModel)
        {

            businessTripsCollectionViewModel.SearchBusinessTripModels = new BusinessTripCollectionModel().LoadOtherBusinessTrips(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("OthersBusinessTrips", businessTripsCollectionViewModel);
        }
    }
}