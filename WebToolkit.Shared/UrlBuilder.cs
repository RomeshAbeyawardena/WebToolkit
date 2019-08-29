using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebToolkit.Shared
{
    public class UrlBuilder : UriBuilder
    {
        public Uri BaseUri { get; }

        public UrlBuilder(string baseUri)
            : this(new Uri(baseUri))
        {
            
        }

        public Uri GetUri(string relativeUrl) => new Uri(BaseUri, relativeUrl);

        public Uri GetUriWithQueryString(string relativeUrl, IDictionary<string, string> queryStringValues)
        {
            return new Uri(GetUri(relativeUrl),
                QueryString.Create(queryStringValues).ToUriComponent());
        }

        public UrlBuilder(Uri baseUri)
            : base(baseUri)
        {
            BaseUri = baseUri;
        }
    }
}