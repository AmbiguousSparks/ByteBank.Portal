using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.Infra.Handlers
{
    public class ActionRequestHandler : RequestHandler
    {
        public override async Task Handle(HttpListenerResponse response, string path, CancellationToken cancellationToken = default)
        {

            var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            var controllerName = pathParts[0];

            var actionName = pathParts[1];

            var actionResponse = GetResponse(controllerName, actionName);

            var contentType = "text/html";

            await WriteResponse(response, contentType, actionResponse);
        }


        #region Privates
        private object GetController(string controllerName)
        {
            var baseNamespace = Assembly.GetEntryAssembly().GetName().Name;

            var controllerFullname = $"{baseNamespace}.Controllers.{controllerName}Controller";

            var controllerWrapper = Activator.CreateInstance(baseNamespace, controllerFullname);

            return controllerWrapper.Unwrap();
        }

        private string GetResponse(string controllerName, string actionName)
        {
            var controller = GetController(controllerName);

            var methodInfo = controller.GetType().GetMethod(actionName);

            var actionResponse = (Task<string>)methodInfo.Invoke(controller, new object[0]);

            return actionResponse.Result;
        } 
        #endregion
    }
}
