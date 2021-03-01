using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MathCalculator
{
    public class Server
    {
        private double _clientNoX;
        private double _clientNoY;
        private double _addedNo;

        public void Start()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 3001;
                IPAddress localAddress = IPAddress.Loopback;
                server = new TcpListener(localAddress, port);
                server.Start();
                Console.WriteLine("Server started");

                DoClient(server);

                server.Stop();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }

        private void DoClient(TcpListener server)
        {
            TcpClient connectionSocket = server.AcceptTcpClient();

            Console.WriteLine("Server activated");
            Stream ns = connectionSocket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            
            sw.WriteLine("Type: Add X Y to add two numbers");
            string[] arr = sr.ReadLine().Split(' ');
            _clientNoX = Double.Parse(arr[1]);
            _clientNoY = Double.Parse(arr[2]);
            _addedNo = _clientNoX + _clientNoY;

            sw.WriteLine($"Result: {_addedNo}");

        }
    }
}
