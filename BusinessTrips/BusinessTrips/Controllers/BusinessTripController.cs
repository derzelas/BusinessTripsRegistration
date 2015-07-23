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

            businessTripModel.User = new UserModel(GetUserIdFromCookie()); 
            businessTripModel.Save();

            var email = new Email();
            email.SendBusinessTripRegistrationEmail(businessTripModel.Id);

            return View("RegisteredSuccessfully");
        }

        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetUserBusinessTrips()
        {
            UserModel userModel = new UserModel(GetUserIdFromCookie());

            var userBusinessTripsCollection =
                new UserBusinessTripsCollectionViewModel(
                    userModel.BusinessTrips.Select(e => new UserBusinessTripViewModel(e)).OrderByDescending(m => m.StartingDate));

            return View("UserBusinessTrips", userBusinessTripsCollection);
        }

        [RoleAuthorize(Role.Regular, Role.Hr)]
        public ActionResult GetDetails(Guid businessTripId)
        {
            BusinessTripModel businessTripModel = new BusinessTripModel(businessTripId);

            if (IsOwnBusinessTrip(businessTripModel.User.Id) || User.IsInRole("HR"))
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
            businessTripsCollectionViewModel.BusinessTrips = 
                new BusinessTripCollectionModel().GetBusinessTripsBy(businessTripsCollectionViewModel.BusinessTripFilter);

            return View("AllBusinessTrips", businessTripsCollectionViewModel);
        }

        [RoleAuthorize(Role.Hr)]
        public ActionResult GetBy(string guid)
        {
            Guid parsedGuid;

            if (Guid.TryParse(guid, out parsedGuid))
            {
                var businessTripsCollectionViewModel = new AllBusinessTripsCollectionViewModel
                {
                    BusinessTripFilter = new BusinessTripFilter
                    {
                        Guid = parsedGuid
                    }
                };

                return RedirectToAction("GetAllBusinessTrips", new { businessTripsCollectionViewModel});
            }

            return View("BusinessTripNotFound");
        }

        [RoleAuthorize(Role.Hr)]
        public ActionResult Accept(Guid businessTripId)
        {
            var businessTripModel = new BusinessTripModel(businessTripId);
            businessTripModel.ChangeStatus(BusinessTripStatus.Accepted);

            return View("StatusChangedSuccessfully");
        }

        [RoleAuthorize(Role.Hr)]
        public ActionResult Reject(Guid businessTripId)
        {
            var businessTripModel = new BusinessTripModel(businessTripId);
            businessTripModel.ChangeStatus(BusinessTripStatus.Rejected);

            return View("StatusChangedSuccessfully");
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

        // There will always be a cookie because of Authorize, so no check for null is required
        private Guid GetUserIdFromCookie()
        {
            string cookieName = ConfigurationManager.AppSettings["Cookie"];

            var cookieValue = Request.Cookies[cookieName].Value;

            return Guid.Parse(FormsAuthentication.Decrypt(cookieValue).Name);
        }

        private bool IsOwnBusinessTrip(Guid ownerId)
        {
            return ownerId == GetUserIdFromCookie();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is BusinessTripNotFoundException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("ErrorEncountered");
            }
        }
    }
}