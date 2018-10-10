using System;
using System.Net.Sockets;

namespace tptcpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 37;
            string ipAddr = args[0];

            TcpClient client = new TcpClient(ipAddr, port);

            NetworkStream networkStream = client.GetStream();

            byte[] receivedBytes = new byte[128];

            networkStream.Read(receivedBytes, 0, receivedBytes.Length);

            uint timeStamp = BitConverter.ToUInt32(receivedBytes, 0);

            DateTime dtDateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();

            Console.WriteLine("Timestamp received from server: " + timeStamp +
                              ". Readable for humans this is the date " + dtDateTime);

            networkStream.Close();
            client.Close();
        }
    }
}
