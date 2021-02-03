using System;
using System.Security.Cryptography;
using System.Text;

namespace Exadel.HEH.Backend.Host.Identity.Security
{
    public static class Hashing
    {
        public static string GetRandomSalt()
        {
            byte[] salt = new byte[64];
            using var random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string HashPasswordWithSalt(string password, string salt)
        {
            var saltedPassword = password + salt;
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));
        }
    }
}