using ByteBank.Portal.Infra.Utils;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra.Handlers
{
    public class FileRequestHandler : RequestHandler
    {
        public override async Task Handle(HttpListenerResponse response, string path, CancellationToken cancellationToken = default)
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

                byte[] bytes = GetBytesFromStream(stream);

                await WriteResponse(response, contentType, bytes);

                stream.Close();
            }
        }
    }
}
