using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;

namespace ByteBank.Infra.Bindings.Inteface
{
    public interface IActionBinder
    {
        /// <summary>
        /// Returns the action binding info containing the method info and the parameteres to be invoked
        /// </summary>
        /// <param name="controller">the controller object</param>
        /// <param name="parameters">the name value collection of the query string parameters</param>
        /// <param name="actionName">the name of the action</param>
        /// <returns></returns>
        Task<ActionBindingInfo> GetActionMethodInfo(object controller, NameValueCollection parameters, string actionName);
    }
}
