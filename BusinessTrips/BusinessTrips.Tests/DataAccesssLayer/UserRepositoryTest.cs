using System;
using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private EFStorage storage;
        private UserRepository repository;
        private UserModel userModel;
        private UserRegistrationModel userRegistrationModel;

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
        public void GetByIdFindsCreatedUser()
        {
            repository.CreateByUserRegistration(userRegistrationModel);
            repository.CommitChanges();

            UserModel retrievedUser = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(userRegistrationModel.Id, retrievedUser.Id);
            Assert.AreEqual(userRegistrationModel.Name, retrievedUser.Name);
            Assert.AreEqual(userRegistrationModel.Email, retrievedUser.Email);
            Assert.AreEqual(userRegistrationModel.Password, retrievedUser.Password);
            Assert.AreEqual(retrievedUser.IsConfirmed, false);
        }

        [TestMethod]
        public void ValidationPassesWhenCredentialsAreValid()
        {
            repository.CreateByUserRegistration(userRegistrationModel);
            repository.CommitChanges();

            bool actual = repository.AreCredentialsValid(userRegistrationModel.Email, userRegistrationModel.Password);
            Assert.AreEqual(actual, true);
        }

        [TestMethod]
        public void ValidationFailsWhenCredentialisAreInvalid()
        {
            userModel.Email = "notexist@work.com";
            userModel.Password = "nopassword";

            Assert.AreEqual(repository.AreCredentialsValid(userModel.Email, userModel.Password), false);
        }

        [TestMethod]
        public void ConfirmSetIsConfirmedPropertyToTrue()
        {
            userModel.Id = userRegistrationModel.Id;

            repository.CreateByUserRegistration(userRegistrationModel);
            repository.CommitChanges();

            repository.Confirm(userModel);
            repository.CommitChanges();
            
            UserModel retrievedModel = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(retrievedModel.IsConfirmed, true);
        }
    }
}