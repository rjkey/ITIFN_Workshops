using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpudpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a IP address.");
                Environment.Exit(1);
            }
            var client = new ClientUdp();
            client.StartQuery(args[0]);
        }
    }
}
