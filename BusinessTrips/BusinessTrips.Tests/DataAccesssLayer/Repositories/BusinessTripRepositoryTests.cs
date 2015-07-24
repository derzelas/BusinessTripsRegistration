using System;
using System.Linq;
using BusinessTrips.DAL.Entities;
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
        private BusinessTripsRepository businessTripsRepository;
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

            businessTripsRepository = new BusinessTripsRepository();

            businessTripModel = new BusinessTripModel
            {
                Id = Guid.NewGuid(),
                EndingDate = DateTime.Now,
                StartingDate = DateTime.Now,
                User = userModel
            };

            businessTripsRepository.SaveChanges();
        }

        [TestMethod]
        public void Add_ElementIsFoundInStorageByHisId()
        {
            businessTripsRepository.Add(businessTripModel);
            businessTripsRepository.SaveChanges();

            var actual = new BusinessTripModel(businessTripsRepository.GetById(businessTripModel.Id));

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

                businessTripsRepository.Add(businessTripModel);
            }

            businessTripsRepository.SaveChanges();

            var actual = businessTripsRepository.GetByUser(userRegistrationModel.Id).Select(e => new BusinessTripModel(e));

            foreach (var tripModel in actual)
            {
                Assert.AreEqual(userRegistrationModel.Id, tripModel.User.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessTripNotFoundException))]
        public void GetBy_NotExistingBusinessTripGui_ThrowsBusinessTripNotFoundException()
        {
            businessTripsRepository.GetById(Guid.NewGuid());
        }

        [TestMethod]
        public void GetBy_BusinessTripIdExistsInStorage_RetursBusinessTripEntity()
        {
            businessTripsRepository.Add(businessTripModel);
            businessTripsRepository.SaveChanges();

            BusinessTripEntity actualBusinessTripEntity = businessTripsRepository.GetById(businessTripModel.Id);

            Assert.AreEqual(businessTripModel.Id, actualBusinessTripEntity.Id);
            Assert.AreEqual(businessTripModel.User.Id, actualBusinessTripEntity.User.Id);
            Assert.AreEqual(businessTripModel.User.Email, actualBusinessTripEntity.User.Email);
        }
    }
}
