using System;
using System.Data.Entity;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {

        private UserRepository repository;
        private UserModel userModel;
        private UserEntity userRegistrationModel;
        private EfStorage efStorage;

        [TestInitialize]
        public void Initialize()
        {
            efStorage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            efStorage.Database.Initialize(true);

            repository = new UserRepository();
            userModel = new UserModel();

            userRegistrationModel = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Foo",
                Email = "example@test.com",
                HashedPassword = "12345",
            };
        }

        [TestMethod]
        public void CreatedUserEntitySameAsUserRegistrationModel()
        {
            repository.CreateByUserEntity(userRegistrationModel);
            repository.CommitChanges();

            var retrievedModel = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(retrievedModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(retrievedModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(retrievedModel.Password, userRegistrationModel.HashedPassword);
            Assert.IsNotNull(retrievedModel.Id);
            Assert.AreEqual(retrievedModel.IsConfirmed, false);
        }

        [TestMethod]
        public void GetByIdFindsCreatedUser()
        {
            repository.CreateByUserEntity(userRegistrationModel);
            repository.CommitChanges();

            UserModel retrievedUser = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(userRegistrationModel.Id, retrievedUser.Id);
            Assert.AreEqual(userRegistrationModel.Name, retrievedUser.Name);
            Assert.AreEqual(userRegistrationModel.Email, retrievedUser.Email);
            Assert.AreEqual(userRegistrationModel.HashedPassword, retrievedUser.Password);
            Assert.AreEqual(retrievedUser.IsConfirmed, false);
        }

        [TestMethod]
        public void ValidationPassesWhenCredentialsAreValid()
        {
            repository.CreateByUserEntity(userRegistrationModel);
            repository.CommitChanges();
            repository.Confirm(userRegistrationModel.Id);
            repository.CommitChanges();

            bool actual = repository.AreCredentialsValid(userRegistrationModel.Email, userRegistrationModel.HashedPassword);
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

            repository.CreateByUserEntity(userRegistrationModel);
            repository.CommitChanges();

            repository.Confirm(userModel.Id);
            repository.CommitChanges();

            UserModel retrievedModel = repository.GetById(userRegistrationModel.Id);

            Assert.AreEqual(retrievedModel.IsConfirmed, true);
        }

        [TestMethod]
        public void GetByEmailIsNotNullWhenEmailExists()
        {
            repository.CreateByUserEntity(userRegistrationModel);
            repository.CommitChanges();

            Assert.IsNotNull(repository.GetByEmail(userRegistrationModel.Email));
        }

        [TestMethod]
        public void GetByEmailReturnNullWhenEmailNotExists()
        {
            string email = "noemail@gmail.com";

            Assert.IsNull(repository.GetByEmail(email));
        }
    }
}