using System;
using System.Linq;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class UserRegistrationModelTests
    {
        private UserRegistrationModel userRegistrationModel;
        private EfStorage storage;

        [TestInitialize]
        public void Initialize()
        {
            storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
                ConfirmedPassword = "password",
            };
        }

        [TestMethod]
        public void Save_AddsNewUserEntityInStorage()
        {
            userRegistrationModel.Save();

            Assert.IsNotNull(storage.Users.SingleOrDefault(entity => entity.Id == userRegistrationModel.Id));
        }

        [TestMethod]
        public void Save_AddedUserEntityInNotConfirmed()
        {
            userRegistrationModel.Save();
            var userEntity = storage.Users.Single(entity => entity.Id == userRegistrationModel.Id);
            
            Assert.IsFalse(userEntity.IsConfirmed);
        }

        [TestMethod]
        public void Save_AddedUserEntityHasHashedPasswordAndAGuidSalt()
        {
            userRegistrationModel.Save();
            var userEntity = storage.Users.Single(entity => entity.Id == userRegistrationModel.Id);
            var salt = Guid.Parse(userEntity.Salt);

            Assert.AreNotEqual(userRegistrationModel.Password,userEntity.HashedPassword);
            Assert.IsInstanceOfType(salt,typeof(Guid));
        }
    }
}
