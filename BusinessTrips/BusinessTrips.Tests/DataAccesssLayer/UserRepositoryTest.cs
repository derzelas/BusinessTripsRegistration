using System;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository repository;
        private UserModel userModel;
        private UserRegistrationModel userRegistrationModel;
        private EFStorage storage;

        [TestInitialize]
        public void Initialize()
        {
            storage = new EFStorage();
            repository = new UserRepository();
            userModel = new UserModel();

            userRegistrationModel = new UserRegistrationModel()
            {
                Id = Guid.NewGuid(),
                Name = "Foo",
                Email = "example@test.com",
                Password = "12345",
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            storage.Database.Delete();
            storage.SaveChanges();
        }

        [TestMethod]
        public void CreatedUserEntitySameAsUserRegistrationModel()
        {
            repository.CreateByUserRegistration(userRegistrationModel);
            repository.CommitChanges();

            var retrievedModel = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(retrievedModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(retrievedModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(retrievedModel.Password, userRegistrationModel.Password);
            Assert.IsNotNull(retrievedModel.Id);
            Assert.AreEqual(retrievedModel.IsConfirmed, false);
        }

        [TestMethod]
        public void ValidationPassesWhenCredentialsAreValid()
        {
            UserRegistrationModel registrationModel = new UserRegistrationModel()
            {
                Email = "example@work.com",
                Password = "12345"
            };
            repository.CreateByUserRegistration(registrationModel);

            bool actual = repository.AreCredentialsValid(registrationModel.Email, registrationModel.Password);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void ValidationFailsWhenCredentialisAreInvalid()
        {
            userModel.Email = "notexist@work.com";
            userModel.Password = "12345";

            Assert.AreEqual(false, repository.AreCredentialsValid(userModel.Email, userModel.Password));
        }

        

        [TestMethod]
        public void GetByIdFindsCreatedUser()
        {
            repository.CreateByUserRegistration(userRegistrationModel);

            UserModel actualUserModel = repository.GetById(userModel.Id);

            Assert.AreEqual(userModel, actualUserModel);
        }
    }
}