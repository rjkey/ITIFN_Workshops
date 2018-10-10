using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tptcpserver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpServer();
            server.StartServer();
        }
    }
}
