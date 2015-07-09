using System;
using System.Security.Policy;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model;
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
        }

        [TestMethod]
        public void RegisterReturnRegisterViewIfUserRegistrationModelIsInvalid()
        {
            var invalidModel = new UserRegistrationModel()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Password = "123",
                ConfirmedPassword = "456",
                Email = "notvalidmail.com"
            };

            var result = controller.Register(invalidModel) as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }
    }
}
