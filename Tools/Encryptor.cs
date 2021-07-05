using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace kontacto_api.Tools
{
    public class Encryptor
    {
        private static string EncryptThat(string value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")).AsParallel());
            }
        }

        public static string Encrypt(string value, int levels)
        {
            var valueToEncrypt = value;

            for (var i = 0; i < levels; i++)
            {
                valueToEncrypt = EncryptThat(valueToEncrypt);
            }

            return valueToEncrypt;
        }
    }
}