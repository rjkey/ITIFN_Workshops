using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tpudpclient
{
    public class ClientUdp
    {
        private Socket clientSocket;
        public ClientUdp()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void StartQuery(string ipaddress)
        {         
            try
            {
                //Parse the ip address
                var serverAddress = IPAddress.Parse(ipaddress);
                var sendbuf = Encoding.UTF8.GetBytes("");
                var ep = new IPEndPoint(serverAddress, 37);
                //Send the empty datagram to server using port 37 on the server
                clientSocket.SendTo(sendbuf, ep);
                var buffer = new byte[256];
                //Wait for response
                var bufferSize = clientSocket.Receive(buffer);
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
