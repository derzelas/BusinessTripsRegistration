using System;
using System.Data.Entity;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
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

        [TestInitialize]
        public void Initialize()
        {
            EfStorage efStorage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            efStorage.Database.Initialize(true);

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
            Assert.AreEqual(userEntity.Email, "email@email.com");
            Assert.AreEqual(userEntity.IsConfirmed, false);
            Assert.AreEqual(userEntity.Id, Guid.Empty);
        }

        [TestMethod]
        public void Save_AdsSaltToPasswordAndCreatesHash_BeforeSendingTheUserToTheRepository()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var randomStringGeneratorMock = new Mock<IRandomStringGenerator>();
            UserRegistrationModel model = new UserRegistrationModel(randomStringGeneratorMock.Object, repositoryMock.Object)
            {
                Password = "abc"
            };
            string salt = "123";
            randomStringGeneratorMock.Setup(m => m.GetString()).Returns(salt);
            string expected = PasswordHasher.HashPassword("abc123");

            model.Save();

            repositoryMock.Verify(m => m.CreateByUserEntity(It.Is<UserEntity>(u => u.HashedPassword == expected)));
        }
    }
}
