using System;
using System.Linq;
using System.Reflection;

namespace ByteBank.Infra.Utils
{
    /// <summary>
    /// an utilities class
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Verify if the path to be handled is a file path
        /// </summary>
        /// <param name="path">the url</param>
        /// <returns>if the url is referecing for a file</returns>
        public static bool IsFile(string path)
        {
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries).Last().Contains(".");
        }

        /// <summary>
        /// Convert the url to a resource name in the assembly
        /// </summary>
        /// <param name="path">the url</param>
        /// <returns>a string containing the full resource name</returns>
        public static string ConvertPathToResourceName(string path)
        {
            var assemblyPrefix = Assembly.GetExecutingAssembly().GetName().Name;
            return $"{assemblyPrefix}{path.Replace('/', '.')}";
        }
        /// <summary>
        /// Get the type of the content
        /// </summary>
        /// <param name="path">the url</param>
        /// <returns>a string containing the content type</returns>
        public static string GetContentType(string path)
        {
            string extension = path.Split('.').Last();
            return extension switch
            {
                "css" => "text/css",
                "js" => "application/js",
                "html" => "text/html",
                _ => throw new NotImplementedException("Content type not implemented!"),
            };
        }
    }
}
