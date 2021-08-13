using ByteBank.Infra.Handlers.Interfaces;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text;

namespace ByteBank.Infra.Handlers
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

        protected static async Task WriteResponse(HttpListenerResponse response, string contentType, string content)
        {

            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            await WriteResponse(response, contentType, contentBytes);
        }

        protected static async Task WriteResponse(HttpListenerResponse response, string contentType, byte[] content)
        {
            if(content == null || content.Length == 0)
            {
                NotFound(response);
                return;
            }    
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
