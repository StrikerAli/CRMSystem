using System.Security.Cryptography;
using System.Text;

namespace CRMSystem.Helpers
{
    public static class PasswordHelper //custom function for hashing password (not used)
    {
        // Hashes the password using SHA-256
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string into bytes
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                // Convert the byte array back into a string (hex format)
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
