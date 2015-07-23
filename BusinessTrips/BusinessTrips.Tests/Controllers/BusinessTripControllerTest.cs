using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class BusinessTripControllerTest
    {
        private BusinessTripController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            controller = new BusinessTripController();
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);
        }

        [TestMethod]
        public void Register_ModelStateIsInvalid_ReturnsRegisterView()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new BusinessTripModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Register", result.ViewName);
        }
    }
}
