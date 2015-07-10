using System;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Models
{
    [TestClass]
    public class UserRegistrationModelTest
    {
        private UserRegistrationModel userRegistrationModel;

        [TestInitialize]
        public void Initialize()
        {
            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
                ConfirmedPassword = "password",
            };
        }

        [TestMethod]
        public void SaveCreatesANewGuid()
        {
            userRegistrationModel.Save();
            Assert.IsNotNull(userRegistrationModel.Id);
        }

        [TestMethod]
        public void ToUserEntityCreatesAValidUserEntity()
        {
            UserEntity userEntity = userRegistrationModel.ToUserEntity();

            Assert.AreEqual(userEntity.Name, "nume");
            Assert.AreEqual(userEntity.Password, "password");
            Assert.AreEqual(userEntity.Email, "email@email.com");
            Assert.AreEqual(userEntity.IsConfirmed, false);
            Assert.AreEqual(userEntity.Id, Guid.Empty);
        }
    }
}
