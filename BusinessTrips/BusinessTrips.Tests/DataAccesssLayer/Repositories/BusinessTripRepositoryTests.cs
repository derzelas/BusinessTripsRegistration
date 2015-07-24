using System;
using System.Linq;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Repositories
{
    [TestClass]
    public class BusinessTripRepositoryTest
    {
        private BusinessTripsRepository repository;
        private BusinessTripModel businessTripModel;
        private UserRegistrationModel userRegistrationModel;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Email = "email@email.email",
                Password = "password"
            };

            userRegistrationModel.Save();

            userModel = new UserModel(userRegistrationModel.Id);

            repository = new BusinessTripsRepository();
            businessTripModel = new BusinessTripModel
            {
                Id = Guid.NewGuid(),
                EndingDate = DateTime.Now,
                StartingDate = DateTime.Now,
                User = userModel
            };

            repository.SaveChanges();
        }

        [TestMethod]
        public void Add_ElementIsFoundInStorageByHisId()
        {
            repository.Add(businessTripModel);
            repository.SaveChanges();

            var actual = new BusinessTripModel(repository.GetById(businessTripModel.Id));

            Assert.AreEqual(businessTripModel.Id, actual.Id);
        }

        [TestMethod]
        public void Add_ElementsAreFoundInStorageByUser()
        {
            for (int i = 0; i < 10; i++)
            {
                businessTripModel = new BusinessTripModel
                {
                    Id = Guid.NewGuid(),
                    EndingDate = DateTime.Now,
                    StartingDate = DateTime.Now,
                    User = userModel
                };

                repository.Add(businessTripModel);
            }

            repository.SaveChanges();

            var actual = repository.GetByUser(userRegistrationModel.Id).Select(e => new BusinessTripModel(e));

            foreach (var tripModel in actual)
            {
                Assert.AreEqual(userRegistrationModel.Id, tripModel.User.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessTripNotFoundException))]
        public void GetBy_InvalidGuid_ThrowsBusinessTripNotFoundException()
        {
            
        }
    }
}
