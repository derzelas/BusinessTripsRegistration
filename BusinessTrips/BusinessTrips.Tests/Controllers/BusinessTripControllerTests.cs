using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using BusinessTrips.DAL.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class BusinessTripControllerTests
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
        public void Submit_ModelStateIsInvalid_ReturnsRegisterView()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Submit(new BusinessTripModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Submit", result.ViewName);
        }

        [TestMethod]
        public void GetAllBusinessTrips_ReturnsAllBusinessTripsView()
        {
            var result = controller.GetAllBusinessTrips() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AllBusinessTrips", result.ViewName);
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
