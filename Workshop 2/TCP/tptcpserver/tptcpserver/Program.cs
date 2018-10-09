using System;

namespace tptcpserver
{
    class Program
    {
        private static bool _running = false;
        static void Main(string[] args)
        {
            Console.WriteLine("Press Q at any time to stop server.");
            TcpServer server = new TcpServer();
            server.StartListeningForTcpClient();
            _running = true;

            while (_running)
            {
                var keyInput = Console.ReadKey();
                switch (keyInput.Key)
                {
                    case ConsoleKey.Q:
                        server.StopListeningForTcpClient();
                        _running = false;
                        Console.WriteLine("Server stopped.");
                        break;
                }
            }
        }
    }
}
