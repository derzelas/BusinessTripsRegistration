using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessTrips.DAL.Models.User
{
    public class Password
    {
        private readonly string clearTextPassword;
        private readonly string salt;

        public Password(string clearTextPassword, string salt)
        {
            this.clearTextPassword = clearTextPassword;
            this.salt = salt;
        }

        public Password(string clearTextPassword)
        {
            this.clearTextPassword = clearTextPassword;
            salt = Guid.NewGuid().ToString();
        }

        public string GetHashed()
        {
            string saltPassword = clearTextPassword + salt;

            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();

            byte[] crypto =
                crypt.ComputeHash(
                    Encoding.UTF8.GetBytes(saltPassword), 0,
                    Encoding.UTF8.GetByteCount(saltPassword));

            foreach (byte bit in crypto)
            {
                hash.Append(bit.ToString("x2"));
            }

            return hash.ToString();
        }

        public string GetSalt()
        {
            return salt;
        }
    }
}
