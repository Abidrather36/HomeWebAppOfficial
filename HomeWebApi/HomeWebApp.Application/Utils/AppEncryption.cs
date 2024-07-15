using System.Security.Cryptography;
using BCrypt.Net;

namespace HomeWebApp.Application.Utils
{
    public static  class AppEncryption
    {
        public static string GetRandomConfirmationCode()
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
        }
    }
}
