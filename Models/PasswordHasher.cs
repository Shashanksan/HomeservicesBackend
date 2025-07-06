using System.Text;
using System;
using System.Security.Cryptography;


namespace Final_Project.Models
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashedPassword = HashPassword(enteredPassword);
            return hashedPassword == storedHash;
        }
    }
}
