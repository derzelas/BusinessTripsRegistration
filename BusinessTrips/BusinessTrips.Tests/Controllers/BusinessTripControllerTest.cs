using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using BusinessTrips.DAL.ViewModel;
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

        [TestMethod]
        public void GetAllBusinessTrips_ReturnsAllBusinessTripsView()
        {
            var result = controller.GetAllBusinessTrips() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AllBusinessTrips", result.ViewName);
        }

        [TestMethod]
        public void GetAllBusinessTrips_NewAllBusinessTripsCollectionViewModel_ReturnsAllBusinessTripsView()
        {
            var allBusinessTripCollection = new AllBusinessTripsCollectionViewModel();
            allBusinessTripCollection.BusinessTripFilter = new BusinessTripFilter();

            var result = controller.GetAllBusinessTrips(allBusinessTripCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AllBusinessTrips", result.ViewName);
        }

        [TestMethod]
        public void GetBy_GuidValid_ReturnsRedirectToGetAllBusinessTripsActionResult()
        {
            string guid = Guid.NewGuid().ToString();

            var result = controller.GetBy(guid) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GetAllBusinessTrips", result.RouteValues["action"]);
        }

        [TestMethod]
        public void GetBy_GuidInvalid_ReturnsBusinessTripNotFoundView()
        {
            string guid = "bad guid";

            var result = controller.GetBy(guid) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("BusinessTripNotFound", result.ViewName);
        }

        [TestMethod]
        public void Accept_BusinessTripIdInStorage_ReturnsStatusChangedSuccessfullyView()
        {
            var businessTripRepository = new BusinessTripsRepository();

            var userRegistrationModel = new UserRegistrationModel();
            userRegistrationModel.Save();

            var businessTripModel = new BusinessTripModel
            {
                User = new UserModel(userRegistrationModel.Id),
                StartingDate = DateTime.Now,
                EndingDate = DateTime.Now
            };

            businessTripRepository.Add(businessTripModel);
            businessTripRepository.SaveChanges();

            var result = controller.Accept(businessTripModel.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("StatusChangedSuccessfully", result.ViewName);
        }
    }
}
