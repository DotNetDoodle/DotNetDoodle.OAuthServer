using System.Security.Cryptography;

namespace DotNetDoodle.OAuthServer.Infrastructure.Jwt
{
    internal static class EncryptionUtility
    {
        internal static byte[] GetRandomBytes(int length)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[256];
                rng.GetBytes(key);
                return key;
            }
        }
    }
}