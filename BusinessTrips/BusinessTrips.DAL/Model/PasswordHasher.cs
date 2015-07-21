using System.Security.Cryptography;
using System.Text;

namespace BusinessTrips.DAL.Model
{
    public class PasswordHasher
    {
        public static string GetHashed(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash.Append(bit.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}