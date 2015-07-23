using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class UserRegistrationModelTest
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
            var userEntity = storage.Users.SingleOrDefault(entity => entity.Id == userRegistrationModel.Id);
            Assert.IsFalse(userEntity.IsConfirmed);
        }

        [TestMethod]
        public void Save_AddedUserEntityHasHashedPasswordAndAGuidSalt()
        {
            userRegistrationModel.Save();
            var userEntity = storage.Users.SingleOrDefault(entity => entity.Id == userRegistrationModel.Id);
            var salt = Guid.Parse(userEntity.Salt);

            Assert.AreNotEqual(userRegistrationModel.Password,userEntity.HashedPassword);
            Assert.IsInstanceOfType(salt,typeof(Guid));
        }

        [TestMethod]
        public void Save_AdsSaltToPasswordAndCreatesHash_BeforeSendingTheUserToTheRepository()
        {
            var repositoryMock = new Mock<IUserRepository>();
            //var randomStringGeneratorMock = new Mock<IRandomSaltGenerator>();
            UserRegistrationModel model = new UserRegistrationModel(repositoryMock.Object)
            {
                Password = "abc"
            };
            string salt = "123";
            //randomStringGeneratorMock.Setup(m => m.GetSalt()).Returns(salt);
            Password password=new Password(model.Password,salt);

            string expected = password.GetHashed();

            model.Save();

            repositoryMock.Verify(m => m.Add(It.Is<UserEntity>(u => u.HashedPassword == expected)));
        }
    }
}
