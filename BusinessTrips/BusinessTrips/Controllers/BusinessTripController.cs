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
            UserModel userModel = new UserModel(Guid.Parse(userId));

            return userModel;
        }

        // There will always be a cookie because of Authorize, so no check for null is required
        private string GetUserIdFromCookie()
        {
            var cookieValue = Request.Cookies[CookieName].Value;

            return FormsAuthentication.Decrypt(cookieValue).Name;
        }

        public ActionResult GetUserBusinessTrips()
        {
            UserModel userModel = GetUserModelById(GetUserIdFromCookie());

            var userBusinessTripsCollection =
                new UserBusinessTripsCollectionViewModel(
                    userModel.BusinessTrips.Select(e => new UserBusinesTripsViewModel(e)));

            return View("UserBusinessTrips", userBusinessTripsCollection);
        }

        public ActionResult Cancel(Guid businessTripId)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel(id);

            if (businessTripModel.Status == BusinessTripStatus.Accepted)
            {
                Email userEmail = new Email();
                userEmail.SendCancelBusinessTripEmail(businessTripModel.Id);
            }

            businessTripModel.ChangeStatus(BusinessTripStatus.Canceled);

            return GetUserBusinessTrips();
        }

        public ActionResult GetDetails(Guid businessTripId)
        {
            BusinessTripModel retreivedModel = new BusinessTripModel(id);

            if (businessTripModel.User.Id.ToString() == HttpContext.User.Identity.Name)
            {
                return View("BusinessTripDetails", businessTripModel);
            }
            return RedirectToAction("GetUserBusinessTrips");
        }

        public ActionResult GetAllBusinessTrips()
        {
            return View("AllBusinessTrips", new AllBusinessTripsCollectionViewModel());
        }

        [HttpPost]
        public ActionResult GetAllBusinessTrips(AllBusinessTripsCollectionViewModel businessTripsCollectionViewModel)
        {
            businessTripsCollectionViewModel.SearchBusinessTripModels = new BusinessTripCollectionModel().LoadOtherBusinessTrips(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("AllBusinessTrips", businessTripsCollectionViewModel);
        }
    }
}