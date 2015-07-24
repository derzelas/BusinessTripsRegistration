using BusinessTrips.DAL.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class PasswordTests
    {
        private const string ClearTextPassword = "password";
        private const string Salt = "salt";

        [TestMethod]
        public void GetHashed_IsDifferentThanClearPassword()
        {
            var password = new Password(ClearTextPassword, "salt");

            Assert.AreNotEqual(ClearTextPassword, password.GetHashed());
        }

        [TestMethod]
        public void GetSalt_ReturnsUsedSalt()
        {                       
            var password = new Password(ClearTextPassword, Salt);

            string actualSalt = password.GetSalt();

            Assert.AreEqual(Salt, actualSalt);
        }

        [TestMethod]
        public void GetHashed_SamePasswordDifferentSalt_ReturnsDifferentHashedPassword()
        {
            var hashedPasswordOne = new Password(ClearTextPassword, Salt).GetHashed();
            var hashedPasswordTwo = new Password(ClearTextPassword, Salt + "2").GetHashed();

            Assert.AreNotEqual(hashedPasswordOne, hashedPasswordTwo);
        }

        [TestMethod]
        public void GetSalt_ReturnsUniqueSaltPerInstance()
        {
            var passwordOne = new Password(ClearTextPassword);
            var passwordTwo = new Password(ClearTextPassword);

            Assert.AreNotEqual(passwordOne.GetSalt(), passwordTwo.GetSalt());
        }
    }
}
