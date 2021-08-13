using ByteBank.Infra.Bindings;
using ByteBank.Infra.Bindings.Inteface;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Infra.Handlers
{
    public class ActionRequestHandler : RequestHandler
    {
        private readonly IActionBinder _actionBinder;

        public ActionRequestHandler()
        {
            _actionBinder = new ActionBinder();
        }

        public override async Task Handle(HttpListenerResponse response,
                                          string path,
                                          NameValueCollection queryString = null,
                                          CancellationToken cancellationToken = default)
        {
            try
            {
                var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

                var controllerName = pathParts[0];

                var actionName = pathParts[1];

                var actionResponse = await GetResponse(controllerName, actionName, queryString);

                var contentType = "text/html";

                await WriteResponse(response, contentType, actionResponse);
            }
            catch (InvalidOperationException)
            {
                NotFound(response);
            }
        }


        #region Privates
        private object GetController(string controllerName)
        {
            var baseNamespace = Assembly.GetEntryAssembly().GetName().Name;

            var controllerFullname = $"{baseNamespace}.Controllers.{controllerName}Controller";

            var controllerWrapper = Activator.CreateInstance(baseNamespace, controllerFullname);

            return controllerWrapper.Unwrap();
        }

        private async Task<string> GetResponse(string controllerName, string actionName, NameValueCollection queryString)
        {
            var controller = GetController(controllerName);

            var actionBindingInfo = await _actionBinder.GetActionMethodInfo(controller, queryString, actionName);

            var actionResponse = (Task<string>)actionBindingInfo.Invoke(controller);

            return actionResponse.Result;
        }
        #endregion
    }
}
