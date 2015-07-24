using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class RecoverPasswordModelTests
    {
        [TestInitialize]
        public void Initialize()
        {
            EfStorage efStorage = new EfStorage(new EfStorageDbInitializerTest());
            efStorage.Database.Initialize(true);            
        }
       
        [TestMethod]
        public void GetId_ReturnsUserIdBasedOnEmail()
        {
            const string email = "email@email.com";

            UserRegistrationModel userRegistrationModel = new UserRegistrationModel
            {
                Name = "nume",
                Email = email,
                Password = "password",
                ConfirmedPassword = "password",
            };
            userRegistrationModel.Save();

            UserEntity expectedUser = new UserRepository().GetBy(email);

            var actualUser = new RecoverPasswordModel
            {
                Email = email
            };
                       
            Assert.AreEqual(expectedUser.Id, actualUser.GetId());
        }
    }
}