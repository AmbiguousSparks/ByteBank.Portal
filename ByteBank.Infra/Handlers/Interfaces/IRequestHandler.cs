﻿using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Infra.Handlers.Interfaces
{
    public interface IRequestHandler
    {
        Task Handle(HttpListenerResponse response, string path, NameValueCollection queryString = null, CancellationToken cancellationToken = default);
    }
}
