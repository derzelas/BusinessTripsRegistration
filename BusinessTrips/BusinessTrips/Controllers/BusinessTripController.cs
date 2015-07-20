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

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(BusinessTripModel businessTripModel)
        {
            if (ModelState.IsValid)
            {
                UserModel userModel = GetUserModelById(GetUserIdFromCookie());

                businessTripModel.User = userModel;

                businessTripModel.Save();

                var email = new Email();
                email.SendBusinessTripRegistrationEmail(businessTripModel.Id);

                return View("RegisteredSuccessfully");
            }
            return View("Register");
        }

        private UserModel GetUserModelById(string userId)
        {
            UserModel userModel = new UserModel();
            userModel.LoadById(userId);

            return userModel;
        }

        // There will always be a cookie because of Authorize, so no check for null is required
        private string GetUserIdFromCookie()
        {
            var cookieValue = Request.Cookies[CookieName].Value;

            return FormsAuthentication.Decrypt(cookieValue).Name;
        }

        public ActionResult ViewUserBusinessTrips()
        {
            var userModel = GetUserModelById(GetUserIdFromCookie());

            var userBusinessTripsCollection = new UserBusinesTripsCollectionViewModel
            {
                UserBusinessTripsViewModels = userModel.BusinessTrips.Select(e => new UserBusinesTripsViewModel(e))
            };

            return View("UserBusinessTrips", userBusinessTripsCollection);
        }

        public ActionResult Cancel(Guid id)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel();
            businessTripModel.LoadById(id);

            if (businessTripModel.Status == BusinessTripStatus.Accepted)
            {
                Email userEmail = new Email();
                userEmail.SendCancelBusinessTripEmail(businessTripModel.Id);
            }

            businessTripModel.ChangeStatus(BusinessTripStatus.Canceled);

            return ViewUserBusinessTrips();
        }

        public ActionResult RequestDetails(Guid id)
        {
            BusinessTripModel retreivedModel = new BusinessTripModel();
            retreivedModel.LoadById(id);

            return View("BusinessTripDetails", retreivedModel);
        }

        public ActionResult Search()
        {
            return View("AllBusinessTrips", new AllBusinessTripsCollectionViewModel());
        }

        [HttpPost]
        public ActionResult Search(AllBusinessTripsCollectionViewModel businessTripsCollectionViewModel)
        {
            businessTripsCollectionViewModel.SearchBusinessTripModels = new BusinessTripCollectionModel().LoadOtherBusinessTrips(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("AllBusinessTrips", businessTripsCollectionViewModel);
        }
    }
}