using ByteBank.Infra.Utils;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Infra.Handlers
{
    public class FileRequestHandler : RequestHandler
    {
        public override async Task Handle(HttpListenerResponse response,
                                          string path,
                                          NameValueCollection queryString = null,
                                          CancellationToken cancellationToken = default)
        {
            var contentType = Util.GetContentType(path);

            string resourceName = Util.ConvertPathToResourceName(path);

            using (var stream = GetResourceStream(resourceName))
            {

                if (stream == null)
                {
                    NotFound(response);
                    return;
                }

                await WriteResponse(response, contentType, stream);

                stream.Close();
            }
        }
    }
}
