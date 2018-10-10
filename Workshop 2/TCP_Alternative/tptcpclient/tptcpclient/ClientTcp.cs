using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tptcpclient
{
    public class ClientTcp
    {
        private Socket clientSocket;

        public ClientTcp()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //TCP setup of socket
        }

        public void StartQuery(string ipaddress)
        {
            try
            {
                //Parse the ip address
                var serverAddress = IPAddress.Parse(ipaddress);
                var sendbuf = Encoding.UTF8.GetBytes("");
                var ep = new IPEndPoint(serverAddress, 37);
                //Connect to the TCP server
                clientSocket.Connect(ep);               
                //Wait for a response
                var buffer = new byte[256];
                clientSocket.Receive(buffer);
                var timeDiff = BitConverter.ToUInt32(buffer, 0);
                Console.WriteLine("Server time: " + new DateTime(1900, 1, 1, 0, 0, 0).AddSeconds(timeDiff));
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Provide a valid IP address: " + fe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                clientSocket.Close();
            }
        }
    }
}
