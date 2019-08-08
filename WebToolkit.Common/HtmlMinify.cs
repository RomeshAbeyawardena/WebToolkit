using System;
using System.Text;
using HtmlAgilityPack;
using WebToolkit.Common.Extensions;

namespace WebToolkit.Common
{
    public static class HtmlMinify
    {
        public static readonly string[] HtmlMinifyReplacementValues = {Environment.NewLine, "\r", "\n", "\t"};
        public static string Minify(string value)
        {
            
            var document = new HtmlDocument();
            document.LoadHtml(value);
            
            var htmlStringBuilder = new StringBuilder(document.DocumentNode.OuterHtml);
            htmlStringBuilder.Append(GetChildNodeHtml(document.DocumentNode.ChildNodes));
                    
            return htmlStringBuilder.ToString();
        }

        private static string GetChildNodeHtml(HtmlNodeCollection childNodes)
        {
            var htmlStringBuilder = new StringBuilder();

            foreach (var documentNodeChildNode in childNodes)
            {
                var replacedValue =
                    documentNodeChildNode.InnerHtml
                        .ReplaceAll(string.Empty, HtmlMinifyReplacementValues)
                        .Trim();
                htmlStringBuilder.Append(replacedValue);

                if (documentNodeChildNode.HasChildNodes)
                    GetChildNodeHtml(documentNodeChildNode.ChildNodes);
            }

            return htmlStringBuilder.ToString();
        }
    }
}