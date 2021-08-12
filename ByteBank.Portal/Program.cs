using ByteBank.Portal.Infra;

namespace ByteBank.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixes = new string[] { "http://localhost:4452/" };
            System.Console.WriteLine("Server started");
            new WebApplication(prefixes).StartAsync();
            System.Console.WriteLine("Server stopped");
        }
    }
}