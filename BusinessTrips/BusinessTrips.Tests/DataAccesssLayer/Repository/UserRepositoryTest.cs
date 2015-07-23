using System;
using System.Data.Entity;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
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
            efStorage = new EfStorage(new EfStorageDbInitializerTest());
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
            repository.Add(userEntity);
            repository.SaveChanges();

            UserEntity retrievedUser = repository.GetBy(userEntity.Id);

            Assert.AreEqual(userEntity.Id, retrievedUser.Id);
            Assert.AreEqual(userEntity.Name, retrievedUser.Name);
            Assert.AreEqual(userEntity.Email, retrievedUser.Email);
            Assert.AreEqual(userEntity.HashedPassword, retrievedUser.HashedPassword);
            Assert.IsFalse(retrievedUser.IsConfirmed);
        }

        [TestMethod]
        public void GetByEmailIsNotNullWhenEmailExists()
        {
            repository.Add(userEntity);
            repository.SaveChanges();

            Assert.IsNotNull(repository.GetBy(userEntity.Email));
        }

        [TestMethod]
        //[ExpectedException(typeof(UserNotFoundException))]
        public void GetByEmailReturnNullWhenEmailNotExists()
        {
            string email = "noemail@gmail.com";

            Assert.IsNull(repository.GetBy(email));
        }
    }
}