namespace WebToolkit.Contracts.Providers
{
    public enum Encoding { Ascii, BigEndianUnicode, Utf32, Utf7, Utf8, Unicode }
    public interface IEncodingProvider
    {
        System.Text.Encoding GetEncoding(Encoding encoding);
        byte[] GetBytes(string value, Encoding encoding);
        string GetString(byte[] bytes, Encoding encoding);
    }
}