using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RobotFahrbefehlServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Application.Run(new Form1());
            TcpListener listen = new TcpListener(IPAddress.Any, 13);
            listen.Start();
            while (true)
            {
                Console.WriteLine("Warte auf Verbindung auf Port " +
                    listen.LocalEndpoint + "...");
                new Thread(new TimeHandler(listen.AcceptTcpClient()).Do).Start();
            }
        }
    }
}