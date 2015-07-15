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
        private UserEntity userEntity;
        private EfStorage efStorage;

        [TestInitialize]
        public void Initialize()
        {
            efStorage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            efStorage.Database.Initialize(true);

            repository = new UserRepository();
            userModel = new UserModel();

            userEntity = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Foo",
                Email = "example@test.com",
                HashedPassword = "12345",
            };
        }

        [TestMethod]
        public void GetByIdFindsCreatedUser()
        {
            repository.CreateByUserEntity(userEntity);
            repository.CommitChanges();

            UserModel retrievedUser = repository.GetById(userEntity.Id);

            Assert.AreEqual(userEntity.Id, retrievedUser.Id);
            Assert.AreEqual(userEntity.Name, retrievedUser.Name);
            Assert.AreEqual(userEntity.Email, retrievedUser.Email);
            Assert.AreEqual(userEntity.HashedPassword, retrievedUser.Password);
            Assert.AreEqual(retrievedUser.IsConfirmed, false);
        }

        [TestMethod]
        public void ConfirmSetIsConfirmedPropertyToTrue()
        {
            userModel.Id = userEntity.Id;

            repository.CreateByUserEntity(userEntity);
            repository.CommitChanges();

            repository.Confirm(userModel.Id);
            repository.CommitChanges();

            UserModel retrievedModel = repository.GetById(userEntity.Id);

            Assert.AreEqual(retrievedModel.IsConfirmed, true);
        }

        [TestMethod]
        public void GetByEmailIsNotNullWhenEmailExists()
        {
            repository.CreateByUserEntity(userEntity);
            repository.CommitChanges();

            Assert.IsNotNull(repository.GetByEmail(userEntity.Email));
        }

        [TestMethod]
        public void GetByEmailReturnNullWhenEmailNotExists()
        {
            string email = "noemail@gmail.com";

            Assert.IsNull(repository.GetByEmail(email));
        }
    }
}