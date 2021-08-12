using ByteBank.Portal.Infra.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace ByteBank.Portal.Infra
{
    internal class WebApplication
    {
        private readonly string[] _prefixes;
        public WebApplication(string[] prefixes)
        {
            _prefixes = prefixes ?? throw new ArgumentNullException(nameof(prefixes));
        }
        public void StartAsync()
        {
            while (true)
                HandleRequest();
        }

        private void HandleRequest()
        {
            var listener = new HttpListener();

            foreach (var prefix in _prefixes)
                listener.Prefixes.Add(prefix);

            listener.Start();

            var context = listener.GetContextAsync().Result;

            var request = context.Request;
            var response = context.Response;

            var path = request.Url.AbsolutePath;

            var resourceName = Util.ConvertPathToResourceName(path);

            try
            {
                var contentType = Util.GetContentType(path);

                var assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(resourceName);

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
    }
}