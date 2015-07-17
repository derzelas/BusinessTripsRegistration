using System;
using System.Web.Mvc;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;

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
                var tripsRepository = new BusinessTripsRepository();
                var retreivedModel = tripsRepository.GetById(parsedGuid);

                if (retreivedModel != null)
                {
                    return View("ManageRequest", retreivedModel);
                }
            }
            return View("RequestNotFound");
        }

        public ActionResult AcceptRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(RequestStatus.Accepted);

            return View("StatusChangedSuccessfully");
        }

        public ActionResult RejectRequest(BusinessTripModel businessTripModel)
        {
            businessTripModel.ChangeStatus(RequestStatus.Rejected);

            return View("StatusChangedSuccessfully");
        }
    }
}