using System;
using System.Net.Sockets;
using System.IO;

namespace SimpleHttpServer {

    class HttpHandler {

        private StreamReader sr;
        private StreamWriter sw;
        private TcpClient client;

        public HttpHandler(TcpClient client) {
            this.client = client;
            sr = new StreamReader(this.client.GetStream());
            sw = new StreamWriter(this.client.GetStream());
        }

        public void Do() {
            Console.WriteLine("Verbindung zu " +
                client.Client.RemoteEndPoint);
            String request = sr.ReadLine();
            Console.WriteLine("Request: " + request);
            if (request.Contains("GET")) {
                while (true) {
                    // Test des MIME header
                    String thisLine = sr.ReadLine();
                    if (thisLine.Trim() == "")
                        break;
                }
                try {
                    String theData;
                    String filename = @"Temp\RobotHttpServer\daten.txt";
                    using (StreamReader file = new StreamReader(filename)) {
                        theData = file.ReadToEnd();
                    }
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Date: " + DateTime.Now.ToString());
                    sw.WriteLine("Server: PlainHttpServer 1.0");
                    sw.WriteLine("Content-length: " + theData.Length);
                    sw.WriteLine("Content-type: text/plain");
                    sw.WriteLine(); // Leerzeile senden
                    sw.WriteLine(theData);
                    sw.Flush();
                    Console.WriteLine("File gesendet");
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    try {
                        sw.WriteLine(e.Message);
                        sw.Flush();
                    }
                    catch (Exception en) {
                        Console.WriteLine(en.Message);
                    }
                }
            }
            client.Close();
        }
    }
}
