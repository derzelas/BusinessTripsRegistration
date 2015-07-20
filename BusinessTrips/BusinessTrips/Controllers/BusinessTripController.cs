using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;
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
                UserModel userModel = GetUserModelByEmail(GetUserEmailFromCookie());

                businessTripModel.User = userModel;

                businessTripModel.Save();

                var email = new Email();
                email.SendBusinessTripRegistrationEmail(businessTripModel.Id);

                return View("BusinessTripAdded");
            }
            return View("RegisterBusinessTrip");
        }

        private UserModel GetUserModelByEmail(string email)
        {
            UserModel userModel = new UserModel();
            userModel.LoadByEmail(email);
            return userModel;
        }

        // There will always be a cookie because of Authorize, so no check for null is required
        private string GetUserEmailFromCookie()
        {
            var cookieValue = Request.Cookies[CookieName].Value;

            return FormsAuthentication.Decrypt(cookieValue).Name;
        }

        public ActionResult ViewMyBusinessTrips()
        {
            var userModel = GetUserModelByEmail(GetUserEmailFromCookie());

            var myBusinessTripsCollection = new PersonalBusinesTripsCollectionViewModel
            {
                MyBusinesTripsViewModels = userModel.BusinessTrips.Select(e => new PersonalBusinesTripsViewModel(e))
            };

            return View("MyBusinessTrips", myBusinessTripsCollection);
        }

        public ActionResult CancelRequest(Guid id)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel();
            businessTripModel.LoadById(id);

            if (businessTripModel.Status == BusinessTripStatus.Accepted)
            {
                Email userEmail = new Email();
                userEmail.SendCancelBusinessTripEmail(businessTripModel.Id);
            }

            businessTripModel.ChangeStatus(BusinessTripStatus.Canceled);

            return ViewMyBusinessTrips();
        }

        public ActionResult RequestDetails(Guid id)
        {
            BusinessTripModel retreivedModel = new BusinessTripModel();
            retreivedModel.LoadById(id);

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