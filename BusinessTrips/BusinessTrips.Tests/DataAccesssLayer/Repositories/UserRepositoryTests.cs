using System;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Repositories
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
        public void GetById_IdInRepository_ReturnsEntity()
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
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetById_IdNotInRepository_ThrowsException()
        {
            repository.GetBy(userEntity.Id);
        }

        [TestMethod]
        public void GetByEmail_ExistentEmail_NoNull()
        {
            repository.Add(userEntity);
            repository.SaveChanges();

            Assert.IsNotNull(repository.GetBy(userEntity.Email));
        }

        [TestMethod]
        public void GetByEmail_InexistentEmail_ReturnsNull()
        {
            string email = "noemail@gmail.com";

            Assert.IsNull(repository.GetBy(email));
        }
    }
}