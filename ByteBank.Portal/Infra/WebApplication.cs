using ByteBank.Portal.Infra.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    internal class WebApplication
    {
        private readonly string[] _prefixes;
        public WebApplication(string[] prefixes)
        {
            _prefixes = prefixes ?? throw new ArgumentNullException(nameof(prefixes));
        }
        public async Task StartAsync()
        {
            while (true)
                await HandleRequestAsync();
        }

        private async Task HandleRequestAsync()
        {
            var listener = new HttpListener();

            foreach (var prefix in _prefixes)
                listener.Prefixes.Add(prefix);

            listener.Start();

            var context = await listener.GetContextAsync();

            var request = context.Request;
            var response = context.Response;

            var path = request.Url.AbsolutePath;

            var resourceName = Util.ConvertPathToResourceName(path);

            try
            {
                var contentType = Util.GetContentType(path);

                var assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                if (stream == null)
                {
                    NotFound(response);
                    listener.Stop();
                    return;
                }

                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                response.ContentType = $"{contentType}; charset=utf-8";
                response.StatusCode = 200;
                response.ContentLength64 = stream.Length;

                response.OutputStream.Write(bytes, 0, bytes.Length);

                response.OutputStream.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            listener.Stop();
        }

        private void NotFound(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            response.OutputStream.Close();
        }
    }
}