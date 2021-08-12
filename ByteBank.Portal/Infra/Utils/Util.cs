﻿using System;
using System.Linq;
using System.Reflection;

namespace ByteBank.Portal.Infra.Utils
{
    internal static class Util
    {
        public static string ConvertPathToResourceName(string path)
        {
            var assemblyPrefix = Assembly.GetExecutingAssembly().GetName().Name;
            return $"{assemblyPrefix}{path.Replace('/', '.')}";
        }

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
