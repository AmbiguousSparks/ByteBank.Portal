using ByteBank.Portal.Infra.Handlers.Interfaces;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ByteBank.Portal.Infra.Handlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        public abstract Task Handle(HttpListenerResponse response, string path, CancellationToken cancellationToken = default);

        #region Support Methods
        protected static Stream GetResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(resourceName);
        }
        protected static byte[] GetBytesFromStream(Stream stream)
        {
            var bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        protected static async Task WriteResponse(HttpListenerResponse response, string contentType, byte[] content)
        {
            using (response.OutputStream)
            {
                response.ContentType = $"{contentType}; charset=utf-8";
                response.StatusCode = 200;
                response.ContentLength64 = content.Length;

                await response.OutputStream.WriteAsync(content.AsMemory(0, content.Length));

                response.OutputStream.Close();
            }
        }

        protected static async Task WriteResponse(HttpListenerResponse response, string contentType, Stream stream)
        {
            using (stream)
            {
                byte[] bytes = GetBytesFromStream(stream);
                await WriteResponse(response, contentType, bytes);
            }
        }

        protected static void NotFound(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            response.OutputStream.Close();
        } 
        #endregion

    }
}
