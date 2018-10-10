﻿using System;
namespace tpudpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide an IP address.");
                Environment.Exit(1);
            }
            var client = new ClientUdp();
            client.StartQuery(args[0]);
        }
    }
}
