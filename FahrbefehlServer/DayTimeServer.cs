using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleDayTimeServer {

    public class DayTimeServer {

        public static void Main() {
            // 1.
            //TcpListener listen = new TcpListener(13);
            // 2.
            //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            //TcpListener listen = new TcpListener(ipAddress, 13);
            // 3.
            //IPAddress ipAddress = Dns.Resolve(Environment.MachineName).AddressList[0];
            //IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 13);
            //TcpListener listen = new TcpListener(ipLocalEndPoint);
            // 4.
            TcpListener listen = new TcpListener(IPAddress.Any, 13);
            listen.Start();
            while (true) {
                Console.WriteLine("Warte auf Verbindung auf Port " +
                    listen.LocalEndpoint + "...");
                new Thread(new TimeHandler(listen.AcceptTcpClient()).Do).Start();
            }
        }
    }
}
