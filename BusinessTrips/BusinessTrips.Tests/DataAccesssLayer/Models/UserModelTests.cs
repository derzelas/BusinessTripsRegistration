using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class UserModelTests
    {
        private UserRegistrationModel userRegistrationModel;
        private readonly EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());

        [TestInitialize]
        public void TestInitialize()
        {            
            storage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
                ConfirmedPassword = "password",
            };

            userRegistrationModel.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void Constructor_InvalidId_ThrowsException()
        {
            new UserModel(new Guid());
        }

        [TestMethod]
        public void Constructor_ValidUserEntity_RetunrsValidUserModel()
        {
            UserEntity userEntity = GetEntity();

            var userModel = new UserModel(userEntity);

            Assert.AreEqual(userEntity.BusinessTrips.Count, userModel.BusinessTrips.Count());
            Assert.AreEqual(userEntity.Email, userModel.Email);
            Assert.AreEqual(userEntity.Name, userModel.Name);
            Assert.AreEqual(userEntity.Id, userModel.Id);
        }

        [TestMethod]
        public void Authenthicate_ValidAndConfirmedUser_ReturnsTrue()
        {
            RegistrationConfirmationModel registrationConfirmationModel = new RegistrationConfirmationModel();

            registrationConfirmationModel.Confirm(userRegistrationModel.Id.ToString());

            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password
            };

            var actual = userModel.Authenthicate();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Authenthicate_ValidAndNotConfirmedUser_ReturnsFalse()
        {
            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password
            };

            var actual = userModel.Authenthicate();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Authenthicate_InexistentEmail_ReturnsFalse()
        {
            UserModel userModel = new UserModel()
            {
                Email = "email@email.com",
                Password = "123"
            };

            var actual = userModel.Authenthicate();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Authenthicate_ExistentConfirmedEmailWrongPassword_ReturnsFalse()
        {
            RegistrationConfirmationModel registrationConfirmationModel = new RegistrationConfirmationModel();
            registrationConfirmationModel.Confirm(userRegistrationModel.Id.ToString());

            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = "incorect"
            };

            var actual = userModel.Authenthicate();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetEntity_EntityExistsInStorage_ReturnsCorrectEntity()
        {
            UserEntity expectedEntity = GetEntity();
            storage.Users.Add(expectedEntity);
            storage.SaveChanges();

            var userModel = new UserModel(expectedEntity.Id);
            
            UserEntity actualEntity = userModel.GetEntity();

            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
        }

        private UserEntity GetEntity()
        {
            return new UserEntity
            {
                BusinessTrips = new List<BusinessTripEntity>(),
                Email = "email@email.com",
                Name = "name",
                Id = new Guid()
            };
        }
    }
}
