using System;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Models
{
    [TestClass]
    public class UserRegistrationTests
    {
        private IStorage<UserRegistrationModel> storage;
        private UserRegistrationModel userRegistrationModel;
        
        [TestInitialize]
        public void Initialize()
        {
            storage = new InMemoryStorage<UserRegistrationModel>();
            userRegistrationModel = new UserRegistrationModel();
        }

        [TestMethod]
        public void WHenGivenUser_CallAddMethod_AddsToUserRegistrationRepository()
        {   
            userRegistrationModel.Email = "sandica_robert@yahoo.ro";
            userRegistrationModel.Name = "robert";
         
            userRegistrationModel.Save();

            
        }
    }
}
