using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;

namespace ByteBank.Infra.Bindings.Inteface
{
    public interface IActionBinder
    {
        Task<ActionBindingInfo> GetActionMethodInfo(object controller, NameValueCollection path, string actionName);
    }
}
