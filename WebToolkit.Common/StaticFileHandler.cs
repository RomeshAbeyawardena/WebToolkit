using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public static class StaticFileHandler
    {
        public static async Task HandleAsync<TStartup>(HttpRequest request, HttpResponse response, RouteData routeData, Action<StaticFileHandlerOptions> staticFileHandlerOptionsAction)
        {
            var options = new StaticFileHandlerOptions();
            staticFileHandlerOptionsAction(options);

            var context = request.HttpContext;
            var mimeTypeProvider = context.RequestServices
                .GetRequiredService<IMimeTypeProvider>();
            var log = context.RequestServices.GetRequiredService<ILogger<TStartup>>();
            var fileProvider = context.RequestServices.GetRequiredService<IFileProvider>();
            var cryptography = context.RequestServices.GetRequiredService<ICryptographyProvider>();
            var formatProvider = context.RequestServices.GetRequiredService<IFormatProvider>();
            routeData.Values.TryGetValue(options.FilenameKey, out var fileNameRoute);

            var fullFilePath = Path.Combine(options.BaseContentPath, fileNameRoute?.ToString());

            if(options.UseLogging)
                log.LogInformation("Requested resource '{0}'. Requesting file '{1}'", fileNameRoute, fullFilePath);

            if (fileProvider.FileExists(fullFilePath))
            {
                var fileInfo = fileProvider.GetFileInfo(fullFilePath);
                response.Headers.Add("Cache-Control",new StringValues(new [] { "private", "max-age=2592000" } ));
                response.Headers.Remove("Server");
                response.Headers.Add("ETag", BitConverter.ToString(cryptography.HashBytes(HashAlgorithm.Sha512, 
                        fileInfo.LastWriteTimeUtc.ToString(formatProvider), System.Text.Encoding.UTF32).ToArray())
                    .Replace("-", string.Empty));

                if(options.UseLogging)
                    log.LogInformation("Resource '{0}' exists ({1} bytes) - Transmitting requested file to client.", fileNameRoute, fileInfo.Length);
                
                response.ContentType = mimeTypeProvider.GetMimeType(fileInfo.Extension);

                await response.SendFileAsync(fullFilePath);

                if(options.UseLogging)
                    log.LogInformation("Resource '{0}' transmitted to client", fileNameRoute);

                return;
            }

            var badResponseText = $"File '{fileNameRoute}' not found";
            response.StatusCode = StatusCodes.Status404NotFound;
            response.ContentType = Constants.MimeType.PlainText;

            if(options.UseLogging)
                log.LogError(badResponseText);

            using (var streamWriter = new StreamWriter(context.Response.Body))
                await streamWriter.WriteAsync(badResponseText);
        }
    }
}