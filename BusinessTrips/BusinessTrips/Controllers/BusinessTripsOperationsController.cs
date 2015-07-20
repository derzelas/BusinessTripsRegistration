using System;
using System.Web.Mvc;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.Controllers
{
    [Authorize(Roles = "HR")]
    public class BusinessTripsOperationsController : Controller
    {
        public ActionResult GetRequestBy(string guid)
        {
            Guid parsedGuid;

            if (Guid.TryParse(guid, out parsedGuid))
            {
                BusinessTripModel retreivedModel = new BusinessTripModel(Guid.Parse(guid));

                return View("ManageRequest", retreivedModel);
            }
            return View("RequestNotFound");
        }

        public ActionResult AcceptRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(BusinessTripStatus.Accepted);

            return View("StatusChangedSuccessfully");
        }

        public ActionResult RejectRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(BusinessTripStatus.Rejected);

            return View("StatusChangedSuccessfully");
        }
    }
}