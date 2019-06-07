using System;
using WebToolkit.Contracts;
using Encoding = System.Text.Encoding;

namespace WebToolkit.Common.Providers
{
    public class EncodingProvider : IEncodingProvider
    {
        public readonly ISwitch<Contracts.Encoding, Encoding> EncodingSwitch;
        public Encoding GetEncoding(Contracts.Encoding encoding)
        {
            return EncodingSwitch.Case(encoding);
        }

        public byte[] GetBytes(string value, Contracts.Encoding encoding)
        {
            var encodingObj = GetEncoding_(encoding);

            return encodingObj.GetBytes(value);
        }

        public string GetString(byte[] bytes, Contracts.Encoding encoding)
        {
            var encodingObj = GetEncoding_(encoding);

            return encodingObj.GetString(bytes);
        }

        public EncodingProvider(ISwitch<Contracts.Encoding, Encoding> encodingSwitch = null)
        {
            if(encodingSwitch == null)
                encodingSwitch = Switch<Contracts.Encoding, Encoding>.Create(defaultValueExpression: () =>  default(Encoding))
                    .CaseWhen(Contracts.Encoding.Ascii, Encoding.ASCII)
                    .CaseWhen(Contracts.Encoding.BigEndianUnicode, Encoding.BigEndianUnicode)
                    .CaseWhen(Contracts.Encoding.Utf32, Encoding.UTF32)
                    .CaseWhen(Contracts.Encoding.Utf7, Encoding.UTF7)
                    .CaseWhen(Contracts.Encoding.Utf8, Encoding.UTF8)
                    .CaseWhen(Contracts.Encoding.Unicode, Encoding.Unicode);

            EncodingSwitch = encodingSwitch;
        }

        private Encoding GetEncoding_(Contracts.Encoding encoding)
        {
            var encodingObj = GetEncoding(encoding);

            if(encodingObj == null)
                throw new ArgumentException("Encoding not found", nameof(encoding));

            return encodingObj;
        }
    }
}