using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class ForgotPasswordModelTest
    {
        private UserRegistrationModel userRegistrationModel;

        [TestInitialize]
        public void Initialize()
        {
            EfStorage efStorage = new EfStorage(new EfStorageDbInitializerTest());
            efStorage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
                ConfirmedPassword = "password",
            };
            userRegistrationModel.Save();
        }

       
        [TestMethod]
        public void ToForgotPasswordModelBy_ForgotPasswordModelGetsValidEmailFromView_ReturnsValidIdByEmail()
        {         
            UserEntity userGiven = new UserRepository().GetBy("email@email.com");
            ForgotPasswordModel result = new ForgotPasswordModel();
            result.Email = "email@email.com";

            result=result.ToForgotPasswordModelBy(result.Email);

            Assert.AreEqual(userGiven.Id,result.Id);            
        }
    }
}
