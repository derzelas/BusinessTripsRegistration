using System.Web.Mvc;
using System.Web.Security;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.Services;

namespace BusinessTrips.Controllers
{
    [Authorize(Roles = "Regular,HR")]
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
                var cookieValue = Request.Cookies["Cookie"].Value;
                string email = FormsAuthentication.Decrypt(cookieValue).Name;

                var repository = new UserRepository();
                var entity = repository.GetByEmail(email);
                businessTripModel.User = entity;
                
                businessTripModel.Save();

                Email userEmail = new Email();
                userEmail.SendEmailToBusinessTripOperator(businessTripModel.Id);

                return View("MyBusinessTrips");
            }
            return View("RegisterBusinessTrip");
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
    }
}