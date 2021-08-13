using ByteBank.Infra.Bindings.Inteface;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ByteBank.Infra.Bindings
{
    public class ActionBinder : IActionBinder
    {
        public async Task<ActionBindingInfo> GetActionMethodInfo(object controller, NameValueCollection queryString, string actionName)
        {
            return await Task.Run(async () =>
            {

                var hasParameters = queryString != null && queryString.Count > 0;

                var methodInfo = await GetMethodByNameAndParameters(controller, queryString, actionName);

                return new ActionBindingInfo(methodInfo, queryString);
            });
        }

        private async Task<MethodInfo> GetMethodByNameAndParameters(object controller, NameValueCollection queryString, string actionName)
        {
            return await Task.Run(() =>
            {
                var methods = GetMethodInfoByName(controller, actionName);

                foreach (var method in methods)
                {
                    var parameters = method.GetParameters();

                    if (queryString != null && parameters.Length != queryString?.Count)
                        continue;

                    var match =
                        parameters.All(n => queryString?.Get(n.Name) != null);

                    if (!match)
                        break;

                    return method;
                }

                throw new InvalidOperationException($"Action {actionName} not found!");
            });
        }

        private MethodInfo[] GetMethodInfoByName(object controller, string actionName)
        {
            var methods =
                controller
                .GetType()
                .GetMethods(
                    BindingFlags.Public |
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.Static);

            return methods.Where(m => m.Name == actionName).ToArray();
        }
    }
}
