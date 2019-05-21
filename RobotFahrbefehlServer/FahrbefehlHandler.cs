using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace RobotFahrbefehlServer
{
    class FahrbefehlHandler
    {
        private TcpClient client;

        public FahrbefehlHandler(TcpClient client)
        {
            this.client = client;
        }

        public void Do()
        {
            Console.WriteLine("Verbindung zu " +
                client.Client.RemoteEndPoint);
            TextWriter tw = new StreamWriter(client.GetStream());
            tw.Write(DateTime.Now.ToString());
            tw.Flush();
            client.Close();
        }
    }
}
