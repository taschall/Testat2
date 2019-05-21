using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleHttpServer {

    class SimpleHttpServer {

        public static void Main() {
            Console.WriteLine("Welcome to Http Server");
            // 1.
            //TcpListener listen = new TcpListener(8080);
            // 2.
            //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            //TcpListener listen = new TcpListener(ipAddress, 8080);
            // 3.
            //IPAddress ipAddress = Dns.Resolve(Environment.MachineName).AddressList[0];
            //IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 8080);
            //TcpListener listen = new TcpListener(ipLocalEndPoint);
            // 4.
            TcpListener listen = new TcpListener(IPAddress.Any, 8080);
            listen.Start();
            while (true) {
                Console.WriteLine("Warte auf Verbindung auf Port " +
                    listen.LocalEndpoint + "...");
                new Thread(new HttpHandler(listen.AcceptTcpClient()).Do).Start();
            }
        }
    }
}
