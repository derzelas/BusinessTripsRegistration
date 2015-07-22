using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Storage;
using BusinessTrips.DAL.ViewModel;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [RoleAuthorize(Role.Regular, Role.Hr)]
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

        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetUserBusinessTrips()
        {
            UserModel userModel = GetUserModelBy(GetUserIdFromCookie());

            var userBusinessTripsCollection =
                new UserBusinessTripsCollectionViewModel(
                    userModel.BusinessTrips.Select(e => new UserBusinessTripViewModel(e)).OrderByDescending(m=>m.StartingDate));

            return View("UserBusinessTrips", userBusinessTripsCollection);
        }

        [RoleAuthorize(Role.Hr)]
        public ActionResult GetPendingBusinessTrips()
        {
            var businessTripsCollectionViewModel = new PendingBusinessTripsCollectionViewModel
            {
                BusinessTrips = new PendingBusinessTripCollectionModel().GetPendingBusinessTrips()
            };

            return View("GetPendingBusinessTrips", businessTripsCollectionViewModel);
        }

        [RoleAuthorize(Role.Regular, Role.Hr)]
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

        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetDetails(Guid businessTripId)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel(businessTripId);

            if (businessTripModel.User.Id.ToString() == HttpContext.User.Identity.Name || User.IsInRole("HR"))
            {
                return View("Details", businessTripModel);
            }

            return RedirectToAction("GetUserBusinessTrips");
        }

        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetAllBusinessTrips()
        {
            return View("AllBusinessTrips", new AllBusinessTripsCollectionViewModel());
        }

        [HttpPost]
        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetAllBusinessTrips(AllBusinessTripsCollectionViewModel businessTripsCollectionViewModel)
        {
            businessTripsCollectionViewModel.BusinessTrips = new BusinessTripCollectionModel().GetBusinessTripsBy(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("AllBusinessTrips", businessTripsCollectionViewModel);
        }

        [RoleAuthorize(Role.Hr)]
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

        [RoleAuthorize(Role.Hr)]
        public ActionResult AcceptRequest(Guid businessTripId)
        {
            var businessTripModel=new BusinessTripModel(businessTripId);
            businessTripModel.ChangeStatus(BusinessTripStatus.Accepted);

            return View("StatusChangedSuccessfully");
        }

        [RoleAuthorize(Role.Hr)]
        public ActionResult RejectRequest(Guid businessTripId)
        {
            var businessTripModel = new BusinessTripModel(businessTripId);
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