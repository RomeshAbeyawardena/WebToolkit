using System.Data;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public class MimeTypeProvider: IMimeTypeProvider
    {
        public MimeTypeProvider()
        {
            _mimeTypeProviderSwitch = Switch<string,string>
                .Create()
                .CaseWhen(Constants.FileType.PortableNetworkGraphics, Constants.MimeType.PortableNetworkGraphics)
                .CaseWhen(Constants.FileType.JointExpertsGroupExtended, Constants.MimeType.JointExpertsGroupExtended)
                .CaseWhen(Constants.FileType.JointExpertsGroup, Constants.MimeType.JointExpertsGroup)
                .CaseWhen(Constants.FileType.Sass, Constants.MimeType.Sass)
                .CaseWhen(Constants.FileType.StyleSheet, Constants.MimeType.StyleSheet)
                .CaseWhen(Constants.FileType.Scss, Constants.MimeType.Scss)
                .CaseWhen(Constants.FileType.JavaScript, Constants.MimeType.JavaScript)
                .CaseWhen(Constants.FileType.PlainText, Constants.MimeType.PlainText)
                .CaseWhen(Constants.FileType.Icon, Constants.MimeType.Icon)
                .CaseWhenDefault(Constants.MimeType.OcetStream);
        }
        public string GetMimeType(string filename)
        {
            return _mimeTypeProviderSwitch
                .Case(filename.ToLower());
        }

        private readonly ISwitch<string, string> _mimeTypeProviderSwitch;
    }
}