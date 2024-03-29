﻿using System;
using System.Net;
using System.Net.Sockets;

namespace tpudpserver
{
    public class UdpServer
    {
        private const int PortNumber = 37;
        private readonly DateTime _dateStart = new DateTime(1900, 1, 1, 0, 0, 0);
        private readonly Socket _serverSocket;

        public UdpServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //Udp setup of socket
            var localEndPoint = new IPEndPoint(IPAddress.Any, PortNumber); //Lister on all available ips and port 37
            _serverSocket.Bind(localEndPoint); //Bind the socket
        }

        public void StartServer()
        {
            try
            {
                while (true)
                {
                    EndPoint endPoint = new IPEndPoint(IPAddress.Any, PortNumber);
                    byte[] buffer = new byte[256];
                    //Blocking call - listening for UDP datagrams on port 37
                    Console.WriteLine("Listening on port 37");
                    var bytesReceived = _serverSocket.ReceiveFrom(buffer, ref endPoint);
                    Console.WriteLine("Received call");
                    if (bytesReceived == 0)
                    {
                        //Calculate time diff
                        var dateNow = DateTime.Now;
                        var timeDiff = (uint)(dateNow - _dateStart).TotalSeconds;
                        buffer = BitConverter.GetBytes(timeDiff);
                        _serverSocket.SendTo(buffer, endPoint);
                        Console.WriteLine("Server Time : " + _dateStart.AddSeconds(timeDiff));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _serverSocket.Close();
            }

        }

    }
}
