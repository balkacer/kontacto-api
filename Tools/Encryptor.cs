using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace kontacto_api.Tools
{
    public class Encryptor
    {
        public static string Encrypt(string value) {
            using (SHA256 hash = SHA256Managed.Create()) {
                return String.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")));
            }
        }
    }
}