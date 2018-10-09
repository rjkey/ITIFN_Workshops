using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_Server
{
    /// <summary>
    /// ITIFN E18
    /// Group 5
    /// </summary>
    public class TcpServer
    {
        private readonly TcpListener _server;
        private readonly int _port = 1234;
        private readonly IPAddress _ipAddress;
        private bool _running = false;

        public TcpServer()
        {
            _ipAddress = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(_ipAddress, _port);
        }

        public async void StartListeningForTcpClient()
        {
            _server.Start();
            Console.WriteLine("TCP server started at " + _ipAddress + ":" + _port);
            _running = true;
            await AcceptIncomingConnections();
        }

        public void StopListeningForTcpClient()
        {
            _running = false;
            _server.Stop();      
        }

        private async Task AcceptIncomingConnections()
        {
            while (_running)
            {
                TcpClient incomingClient = await _server.AcceptTcpClientAsync();
                Thread t = new Thread(HandleIncomingConnection);
                t.Start(incomingClient);              
            }
        }

        public void HandleIncomingConnection(object obj)
        {
            TcpClient incomingClient = (TcpClient) obj;

            Console.WriteLine("New connection from " + incomingClient.Client.RemoteEndPoint);

            NetworkStream networkStream = incomingClient.GetStream();

            DateTime now = DateTime.Now;

            int timestamp = (int) now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Console.WriteLine("Timestamp " + timestamp + " sent to client. Readable for humans this is the date " +
                              now);

            byte[] data = BitConverter.GetBytes(timestamp);

            networkStream.Write(data, 0, data.Length);

            networkStream.Close();

            Console.WriteLine("Connection to client closed.");
        }
    }
}