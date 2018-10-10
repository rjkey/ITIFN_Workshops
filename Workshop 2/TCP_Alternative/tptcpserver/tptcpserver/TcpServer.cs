using System;
using System.Net;
using System.Net.Sockets;

namespace tptcpserver
{
    public class TcpServer
    {
        private const int PortNumber = 37;
        private readonly DateTime _dateStart = new DateTime(1900, 1, 1, 0, 0, 0);
        private readonly Socket _serverListener;

        public TcpServer()
        {
            _serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Setup for TCP
            var localEndPoint = new IPEndPoint(IPAddress.Any, PortNumber);
            _serverListener.Bind(localEndPoint); //Bind to the socket
            _serverListener.Listen(5); //Start socket in TCP listening state
        }

        public void StartServer()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("TCP - Listening on port 37");
                    //Blocking call - listening on port 37
                    var socketConnection = _serverListener.Accept();
                    Console.WriteLine("Received call");

                    //Calculate time diff
                    var dateNow = DateTime.Now;
                    var timeDiff = (uint)(dateNow - _dateStart).TotalSeconds;
                    var buffer = BitConverter.GetBytes(timeDiff);
                    socketConnection.Send(buffer);
                    Console.WriteLine("Server Time : " + _dateStart.AddSeconds(timeDiff));
                    socketConnection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _serverListener.Close();
            }

        }
    }
}
