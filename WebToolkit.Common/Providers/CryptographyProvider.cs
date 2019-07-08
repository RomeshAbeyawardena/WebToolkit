using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;
using Encoding = System.Text.Encoding;
using HashAlgorithm = WebToolkit.Contracts.Providers.HashAlgorithm;

namespace WebToolkit.Common.Providers
{
    public class CryptographyProvider : ICryptographyProvider
    {
        public IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, IEnumerable<byte> sourceBytes, IEnumerable<byte> saltBytes = null)
        {
            return _hashAlgorithmSwitch.Case(hashAlgorithm).Invoke(sourceBytes, saltBytes);
        }

        public IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, string value, Encoding encoding, IEnumerable<byte> saltBytes = null)
        {
            return HashBytes(hashAlgorithm, encoding.GetBytes(value), saltBytes);
        }

        public IEnumerable<byte> Encrypt(string value, IEnumerable<byte> salt, IEnumerable<byte> initialVector, Encoding encoding)
        {
            var keyArray = salt.ToArray();

            if(keyArray.Length > 128)
                throw new ArgumentException($"{nameof(keyArray)} must not exceed a length of 128.");

            var iVArray = initialVector.ToArray();

            if(iVArray.Length != 16)
                throw new ArgumentException($"{nameof(iVArray)} must have a length of 16.");

            using (var encryption = new RijndaelManaged())
            {
                
                using (var encryptor = encryption.CreateEncryptor(keyArray, iVArray))
                {
                    var encodedBytes = encoding.GetBytes(value);
                    return encryptor.TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);
                }
            }
        }

        public string Decrypt(IEnumerable<byte> value, IEnumerable<byte> salt, IEnumerable<byte> initialVector, Encoding encoding)
        {
            var valueArray = value.ToArray();

            if(valueArray.Length < 1)
                throw new ArgumentException($"{nameof(valueArray)} must have a length above zero.");

            var iVArray = initialVector.ToArray();

            if(iVArray.Length != 16)
                throw new ArgumentException($"{nameof(iVArray)} must have a length of 16.");

            var keyArray = salt.ToArray();
            
            if(keyArray.Length > 128)
                throw new ArgumentException($"{nameof(keyArray)} must not exceed a length of 128.");

            using (var encryption = new RijndaelManaged())
            {
                using (var encryptor = encryption.CreateDecryptor(keyArray, iVArray))
                {
                    return encoding.GetString(encryptor.TransformFinalBlock(valueArray, 0, valueArray.Length));
                }
            }
        }

        public CryptographyProvider()
        {
            _hashAlgorithmSwitch = Switch<HashAlgorithm, Func<IEnumerable<byte>, IEnumerable<byte>, IEnumerable<byte>>>
                .Create();

                _hashAlgorithmSwitch.CaseWhen(HashAlgorithm.Md5, (b, c) => {
                    using (var md5 = MD5.Create())
                    {
                        return md5.ComputeHash(b.ToArray());
                    }
                }).CaseWhen(HashAlgorithm.Sha512, (b, c) => {
                    using (var sha512 = SHA512.Create())
                    {
                        return sha512.ComputeHash(b.ToArray());
                    }
                }).CaseWhen(HashAlgorithm.PasswordBytes, (b, c) => {
                    if(c== null)
                        throw new ArgumentNullException(nameof(c), "Salt required for this operation");

                    using (var passwordBytes = new Rfc2898DeriveBytes(b.ToArray(), c.ToArray(), 10000))
                    {
                        return passwordBytes.GetBytes(128);
                    }
                });
        }

        private readonly ISwitch<HashAlgorithm, Func<IEnumerable<byte>, IEnumerable<byte>,  IEnumerable<byte>>> _hashAlgorithmSwitch;
    }
}