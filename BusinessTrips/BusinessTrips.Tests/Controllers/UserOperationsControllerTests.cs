using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class UserOperationsControllerTests
    {
        private UserOperationsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            controller = new UserOperationsController();
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);
        }

        [TestMethod]
        public void Register_UserRegistrationModelIsInvalid_ReturnsRegisterView()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistration_GuidIsValidForExistentUser_ReturnRegistrationConfirmationSuccessfulView()
        {
            var userRegistrationModel = new UserRegistrationModel();
            userRegistrationModel.Save();

            var result = controller.ConfirmRegistration(userRegistrationModel.Id.ToString()) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("RegistrationConfirmationSuccessful", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void ConfirmRegistration_GuidIsValidAndNoUser_ThrowsUserNotFoundException()
        {
            controller.ConfirmRegistration(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void Login_UserIsNotInStorage_ReturnsUnknownUserView()
        {
            var result = controller.Login(new UserModel() { Id = Guid.NewGuid(), Password = "" }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("InvalidUser", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void ForgotPassword_UserIsInvalid_ReturnsEmailSentView()
        {
            controller.ForgotPassword(new RecoverPasswordModel
            {
                Email = string.Empty
            });            
        }
    }
}
