using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace HomeWebApp.Application.Utils
{
    public  class AppEncryption
    {
        #region bcrypt
        /* public static string GetRandomConfirmationCode()
         {
             return RandomNumberGenerator.GetInt32(1111, 9999).ToString();
         }


         public static string HashPassword(string password, string salt)
         {
             return BCrypt.Net.BCrypt.HashPassword(password, salt);
         }



         public static string GenerateSalt()
         {
             return BCrypt.Net.BCrypt.GenerateSalt();
         }*/
        #endregion

        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new();
            byte[] salt = new byte[32];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);

        }

        public static string CreatePasswordHash(string password ,string salt)
        {
            var saltedPassword=string.Concat(password, salt);
            HMACSHA256 sha = new();
            byte[] hash=sha.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

            return Convert.ToBase64String(hash);
        }

        public static bool ComparePassword(string dbPassword,string newPassword, string dbSalt)
        {
            return dbPassword==CreatePasswordHash(newPassword,dbSalt);
        } 
    }
}
