using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ByteBank.Application.Abstracts
{
    public abstract class Controller
    {
        protected async Task<string> View([CallerMemberName] string fileName = null)
        {
            var type = GetType();

            var entryAssembly = Assembly.GetEntryAssembly();
            var baseNamespace = entryAssembly.GetName().Name;

            var resourceName = $"{baseNamespace}.View.{type.Name.Replace("Controller", "")}.{fileName}.html";

            using (var streamResource = new StreamReader(entryAssembly.GetManifestResourceStream(resourceName)))
            {

                var pagetext = await streamResource.ReadToEndAsync();

                streamResource.Close();

                return pagetext;
            }
        }
    }
}
