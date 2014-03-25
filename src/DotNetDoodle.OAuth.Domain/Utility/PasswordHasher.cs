
namespace DotNetDoodle.OAuth.Domain.Utility
{
    internal static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword))
            {
                return true;
            }

            return false;
        }
    }
}