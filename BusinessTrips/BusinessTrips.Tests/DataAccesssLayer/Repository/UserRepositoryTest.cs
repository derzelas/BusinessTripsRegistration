using System;
using System.Data.Entity;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Repository
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository repository;
        private UserEntity userEntity;
        private EfStorage efStorage;

        [TestInitialize]
        public void Initialize()
        {
            efStorage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            efStorage.Database.Initialize(true);

            repository = new UserRepository();

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

            UserEntity retrievedUser = repository.GetById(userEntity.Id);

            Assert.AreEqual(userEntity.Id, retrievedUser.Id);
            Assert.AreEqual(userEntity.Name, retrievedUser.Name);
            Assert.AreEqual(userEntity.Email, retrievedUser.Email);
            Assert.AreEqual(userEntity.HashedPassword, retrievedUser.HashedPassword);
            Assert.IsFalse(retrievedUser.IsConfirmed);
        }

        [TestMethod]
        public void GetByEmailIsNotNullWhenEmailExists()
        {
            repository.CreateByUserEntity(userEntity);
            repository.CommitChanges();

            Assert.IsNotNull(repository.GetByEmail(userEntity.Email));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetByEmailReturnNullWhenEmailNotExists()
        {
            string email = "noemail@gmail.com";

            repository.GetByEmail(email);
        }
    }
}