using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.ViewModel;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        [Authorize(Roles = "Regular,HR")]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Authorize(Roles = "Regular,HR")]
        public ActionResult Register(BusinessTripModel businessTripModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            UserModel userModel = GetUserModelBy(GetUserIdFromCookie());

            businessTripModel.User = userModel;
            businessTripModel.Save();

            var email = new Email();
            email.SendBusinessTripRegistrationEmail(businessTripModel.Id);

            return View("RegisteredSuccessfully");
        }

        [Authorize(Roles = "Regular,HR")]
        public ActionResult GetUserBusinessTrips()
        {
            UserModel userModel = GetUserModelBy(GetUserIdFromCookie());

            var userBusinessTripsCollection =
                new UserBusinessTripsCollectionViewModel(
                    userModel.BusinessTrips.Select(e => new UserBusinessTripViewModel(e)));

            return View("UserBusinessTrips", userBusinessTripsCollection);
        }

        [Authorize(Roles = "Regular,HR")]
        public ActionResult Cancel(Guid businessTripId)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel(businessTripId);

            if (businessTripModel.Status == BusinessTripStatus.Accepted)
            {
                Email userEmail = new Email();
                userEmail.SendCancelBusinessTripEmail(businessTripModel.Id);
            }

            businessTripModel.ChangeStatus(BusinessTripStatus.Canceled);

            return GetUserBusinessTrips();
        }

        [Authorize(Roles = "Regular,HR")]
        public ActionResult GetDetails(Guid businessTripId)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel(businessTripId);

            if (businessTripModel.User.Id.ToString() == HttpContext.User.Identity.Name || User.IsInRole("HR"))
            {
                return View("Details", businessTripModel);
            }

            return RedirectToAction("GetUserBusinessTrips");
        }

        [Authorize(Roles = "Regular,HR")]
        public ActionResult GetAllBusinessTrips()
        {
            return View("AllBusinessTrips", new AllBusinessTripsCollectionViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Regular,HR")]
        public ActionResult GetAllBusinessTrips(AllBusinessTripsCollectionViewModel businessTripsCollectionViewModel)
        {
            businessTripsCollectionViewModel.BusinessTrips = new BusinessTripCollectionModel().GetBusinessTripsBy(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("AllBusinessTrips", businessTripsCollectionViewModel);
        }

        [Authorize(Roles = "HR")]
        public ActionResult GetBy(string guid)
        {
            Guid parsedGuid;

            if (Guid.TryParse(guid, out parsedGuid))
            {
                BusinessTripModel businessTripModel = new BusinessTripModel(Guid.Parse(guid));

                return View("ManageRequest", businessTripModel);
            }

            return View("RequestNotFound");
        }

        [Authorize(Roles = "HR")]
        public ActionResult AcceptRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(BusinessTripStatus.Accepted);

            return View("StatusChangedSuccessfully");
        }

        [Authorize(Roles = "HR")]
        public ActionResult RejectRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(BusinessTripStatus.Rejected);

            return View("StatusChangedSuccessfully");
        }

        private static UserModel GetUserModelBy(string userId)
        {
            return new UserModel(Guid.Parse(userId));
        }

        // There will always be a cookie because of Authorize, so no check for null is required
        private string GetUserIdFromCookie()
        {
            string cookieName = ConfigurationManager.AppSettings["Cookie"];

            var cookieValue = Request.Cookies[cookieName].Value;

            return FormsAuthentication.Decrypt(cookieValue).Name;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is BusinessTripNotFoundException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("ErrorEncountered");
            }

            base.OnException(filterContext);
        }
    }
}