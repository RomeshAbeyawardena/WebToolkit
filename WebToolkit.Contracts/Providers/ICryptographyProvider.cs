using System.Collections.Generic;


namespace WebToolkit.Contracts.Providers
{
    public enum HashAlgorithm
    {
        Md5,
        Sha512
    }
    public interface ICryptographyProvider
    {
        IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, IEnumerable<byte> sourceBytes);
        IEnumerable<byte> HashBytes(HashAlgorithm hashAlgorithm, string value, System.Text.Encoding encoding);
    }
}