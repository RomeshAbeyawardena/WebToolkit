using System;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;
using Encoding = System.Text.Encoding;

namespace WebToolkit.Common.Providers
{
    public class EncodingProvider : IEncodingProvider
    {
        public readonly ISwitch<Contracts.Providers.Encoding, Encoding> EncodingSwitch;
        public Encoding GetEncoding(Contracts.Providers.Encoding encoding)
        {
            return EncodingSwitch.Case(encoding);
        }

        public byte[] GetBytes(string value, Contracts.Providers.Encoding encoding)
        {
            var encodingObj = GetEncoding_(encoding);

            return encodingObj.GetBytes(value);
        }

        public string GetString(byte[] bytes, Contracts.Providers.Encoding encoding)
        {
            var encodingObj = GetEncoding_(encoding);

            return encodingObj.GetString(bytes);
        }

        public EncodingProvider(ISwitch<Contracts.Providers.Encoding, Encoding> encodingSwitch)
        {
            EncodingSwitch = encodingSwitch;
        }

        private Encoding GetEncoding_(Contracts.Providers.Encoding encoding)
        {
            var encodingObj = GetEncoding(encoding);

            if(encodingObj == null)
                throw new ArgumentException("Encoding not found", nameof(encoding));

            return encodingObj;
        }
    }
}