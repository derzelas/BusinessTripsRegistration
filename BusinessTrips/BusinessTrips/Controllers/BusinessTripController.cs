using System.Web.Mvc;
using BusinessTrips.DAL.Model;
 
namespace BusinessTrips.Controllers
{
    public class BusinessTripController : Controller
    {
        //
        // GET: /BusinessTrip/
        public ActionResult AddBusinessTrip()
        {
            return View("RegisterBusinessTrip");
        }

        [HttpPost]
        public ActionResult AddBusinessTrip(BusinessTripModel businessTripModel)
        {
            if (ModelState.IsValid)
            {
                businessTripModel.Save();
                //send e-mail to BTO
                //return a view => Business trip added
            }
            // return a view => business trip couldn't be added

            return View("RegisterBusinessTrip");
        }

       
    }
}