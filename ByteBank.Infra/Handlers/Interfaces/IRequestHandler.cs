using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Infra.Handlers.Interfaces
{
    public interface IRequestHandler
    {
        /// <summary>
        /// Handles the request writing on the response
        /// </summary>
        /// <param name="response">The response to be written</param>
        /// <param name="path">the url</param>
        /// <param name="queryString">the query strig parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task Handle(HttpListenerResponse response, string path, NameValueCollection queryString = null, CancellationToken cancellationToken = default);
    }
}
