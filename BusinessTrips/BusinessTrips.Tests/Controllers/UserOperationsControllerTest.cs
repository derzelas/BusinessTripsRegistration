using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class UserOperationsControllerTest
    {
        private UserOperationsController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new UserOperationsController();
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);
        }

        [TestMethod]
        public void RegisterReturnRegisterViewIfUserRegistrationModelIsInvalid()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationSetIsConfirmedPropertyToTrueIfUserGuidExistsAndIsValid()
        {
            var userRegistrationModel = new UserRegistrationModel();
            userRegistrationModel.Save();

            var result = controller.ConfirmRegistration(userRegistrationModel.Id.ToString()) as ViewResult;
            var repository = new UserRepository();
            var userModel = new UserModel(repository.GetById(userRegistrationModel.Id));

            Assert.AreEqual(userModel.IsConfirmed, true);
            Assert.AreEqual("ConfirmRegistration", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidIsEmpty()
        {
            var result = controller.ConfirmRegistration(string.Empty) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidHasBadFormat()
        {
            string badFormatGuid = "5746876876876";

            var result = controller.ConfirmRegistration(badFormatGuid) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void LoginReturnsUnknownUserViewWhenUserIsNotInDatabase()
        {
            var result = controller.Login(new UserModel() { Id = Guid.NewGuid(), Password = "" }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("UnknownUser", result.ViewName);
        }

        //[TestMethod]
        //public void LoginRedirectToRegisterBusinessTripActionWhenUserIsInDatabase()
        //{
        //    var userRegistrationModel = new UserRegistrationModel()
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = "example@gmail.com",
        //        Name = "name",
        //        Password = "password",
        //        ConfirmedPassword = "password"
        //    };
        //    userRegistrationModel.Save();

        //    var repository = new UserRepository();

        //    repository.Confirm(userRegistrationModel.Id);
        //    repository.CommitChanges();

        //    var userModel = repository.GetById(userRegistrationModel.Id);
        //    userModel.Password = "password";

        //    var result = controller.Login(userModel) as RedirectResult;

        //    Assert.AreEqual("RegisterBusinessTrip", result.Url);
        //}
    }
}
