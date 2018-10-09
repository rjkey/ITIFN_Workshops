using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpudpserver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new UdpServer();
            server.StartServer();
        }
    }
}
