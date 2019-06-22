using System;
using System.Security.Cryptography;
using LoansManager.Services.ServicesContracts;
using LoansManager.Common;

namespace LoansManager.Services.Implementations.Services
{
    public class EncrypterService : IEncypterService
    {
        private const int DeriveBytesIterationsCount = 10000;
        private const int SaltSize = 40;

        public string GetSalt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Can not generate salt from an empty value.", nameof(value));
            }

            var saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
            }

            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Can not use an empty salt from hashing value.", nameof(value));
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
