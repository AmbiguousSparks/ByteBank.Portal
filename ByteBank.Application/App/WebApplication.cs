using ByteBank.Infra.Handlers;
using ByteBank.Infra.Handlers.Interfaces;
using ByteBank.Infra.Utils;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace ByteBank.Application.App
{
    public class WebApplication
    {
        #region Fields
        private readonly string[] _prefixes;
        private readonly IRequestHandler _fileRequestHandler;
        private readonly IRequestHandler _actionRequestHandler;
        #endregion

        #region Constructors

        public WebApplication(string[] prefixes)
        {
            _prefixes = prefixes ?? throw new ArgumentNullException(nameof(prefixes));
            _fileRequestHandler = new FileRequestHandler();
            _actionRequestHandler = new ActionRequestHandler();
        }
        #endregion

        public async Task StartAsync()
        {
            while (true)
                await HandleRequestAsync();
        }

        #region Privates
        private HttpListener GetHttpListener()
        {
            var listener = new HttpListener();
            foreach (var prefix in _prefixes)
                listener.Prefixes.Add(prefix);
            return listener;
        }        

        private async Task HandleRequestAsync()
        {
            var listener = GetHttpListener();

            listener.Start();

            var context = await listener.GetContextAsync();

            var request = context.Request;

            var response = context.Response;         
            
            try
            {
                var path = request.Url.AbsolutePath;

                if (Util.IsFile(path))
                {
                    await _fileRequestHandler.Handle(response, path);

                    return;
                }
                await _actionRequestHandler.Handle(response, path, request.QueryString);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                listener.Stop();
            }
        }
        #endregion
    }
}