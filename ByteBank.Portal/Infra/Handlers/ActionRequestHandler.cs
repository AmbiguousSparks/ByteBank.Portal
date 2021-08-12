using ByteBank.Portal.Controllers;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra.Handlers
{
    public class ActionRequestHandler : RequestHandler
    {
        public override async Task Handle(HttpListenerResponse response, string path, CancellationToken cancellationToken = default)
        {

            var contentType = "text/html";

            var controller = new ExchangeController();

            string content;
            switch (path)
            {
                case "/Exchange/MXN":
                    content = await controller.MXN();
                    break;

                case "/Exchange/USD":
                    content = await controller.USD();
                    break;
                default:
                    NotFound(response);
                    return;
            }

            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            await WriteResponse(response, contentType, contentBytes);
        }
    }
}
