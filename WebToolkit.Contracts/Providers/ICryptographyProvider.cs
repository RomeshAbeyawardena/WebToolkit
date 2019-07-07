using System.Collections.Generic;


namespace WebToolkit.Contracts.Providers
{
    public enum HashAlgorithm
    {
        Md5,
        Sha512,
        PasswordBytes
    }
    public interface ICryptographyProvider
    {
        string Decrypt(IEnumerable<byte> value, IEnumerable<byte> salt, IEnumerable<byte> initialVector, System.Text.Encoding encoding);
        IEnumerable<byte> Encrypt(string value, IEnumerable<byte> salt, IEnumerable<byte> initialVector, System.Text.Encoding encoding);
        IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, IEnumerable<byte> sourceBytes, IEnumerable<byte> saltBytes = null);
        IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, string value, System.Text.Encoding encoding, IEnumerable<byte> saltBytes = null);
    }
}