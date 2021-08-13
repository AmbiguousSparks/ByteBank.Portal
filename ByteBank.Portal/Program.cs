using ByteBank.Application.App;
using System;

namespace ByteBank.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixes = new string[] { "http://localhost:4452/" };
            Console.WriteLine("Server started");
            new WebApplication(prefixes).StartAsync().Wait();
            Console.WriteLine("Server stopped");
        }
    }
}