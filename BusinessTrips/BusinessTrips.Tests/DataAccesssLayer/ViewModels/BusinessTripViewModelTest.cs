using System;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.ViewModels
{
    [TestClass]
    public class BusinessTripViewModelTest
    {
        [TestMethod]
        public void Constructor_GivenABusinessTripEntity_InitializationSuccesful()
        {
            BusinessTripEntity businessTripEntity = new BusinessTripEntity()
            {
                StartingDate = DateTime.Now,
                ClientLocation = "Location",
                ClientName = "Name",
                MeansOfTransportation = "Car",
                Accomodation = "acc",
                Status = BusinessTripStatus.Accepted,
                Id = Guid.NewGuid(),
                User = new UserEntity() { Name = "Name"}
            };

            var businessTripViewModel = new BusinessTripViewModel(businessTripEntity);

            Assert.AreEqual(businessTripViewModel.EndingDate, businessTripEntity.StartingDate);
            Assert.AreEqual(businessTripViewModel.Location, businessTripEntity.ClientLocation);
            Assert.AreEqual(businessTripViewModel.Name, businessTripEntity.User.Name);
            Assert.AreEqual(businessTripViewModel.MeansOfTransportaion, businessTripEntity.MeansOfTransportation);
            Assert.AreEqual(businessTripViewModel.Accomodation, businessTripEntity.Accomodation);
            Assert.AreEqual(businessTripViewModel.Status, businessTripEntity.Status);
            Assert.AreEqual(businessTripViewModel.Id, businessTripEntity.Id);
        }
    }
}
