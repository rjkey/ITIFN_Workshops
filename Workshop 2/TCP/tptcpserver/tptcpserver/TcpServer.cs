﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace tptcpserver
{
    /// <summary>
    /// ITIFN E18
    /// Group 5
    /// </summary>
    public class TcpServer
    {
        private readonly TcpListener _server;
        private readonly int _port = 37;
        private bool _running = false;

        public TcpServer()
        {
            _server = new TcpListener(IPAddress.Any,_port);
        }

        public async void StartListeningForTcpClient()
        {
            _server.Start();
            Console.WriteLine("TCP server started at " + _server.LocalEndpoint);
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
            TcpClient incomingClient = (TcpClient)obj;

            Console.WriteLine("New connection from " + incomingClient.Client.RemoteEndPoint);

            NetworkStream networkStream = incomingClient.GetStream();

            DateTime now = DateTime.Now;

            uint timestamp = (uint)now.Subtract(new DateTime(1900, 1, 1, 0, 0, 0)).TotalSeconds;

            Console.WriteLine("Timestamp " + timestamp + " sent to client. Readable for humans this is the date " +
                              now);

            byte[] data = BitConverter.GetBytes(timestamp);

            networkStream.Write(data, 0, data.Length);

            networkStream.Close();

            Console.WriteLine("Connection to client closed.");
        }
    }
}