using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1234;
            string ipAddr = args[0];

            TcpClient client = new TcpClient(ipAddr, port);

            NetworkStream networkStream = client.GetStream();

            byte[] receivedBytes = new byte[128];

            networkStream.Read(receivedBytes,0, receivedBytes.Length);

            int timeStamp = BitConverter.ToInt32(receivedBytes, 0);

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();

            Console.WriteLine("Timestamp received from server: " + timeStamp +
                              ". Readable for humans this is the date " + dtDateTime);

            networkStream.Close();
            client.Close();
        }
    }
}
