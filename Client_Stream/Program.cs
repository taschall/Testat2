using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Client_Stream
{
    class Program
    {


        const int PORT_NO = 13;
        const string SERVER_IP = "192.168.1.13";
       

        static void Main(string[] args)
        {
            //---data to send to the server---
            string textToSend = "TrackLine " + 10.23;

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

            StreamWriter sw = new StreamWriter(client.GetStream());
            //sw.WriteLine(textToSend);

            //sw.Flush();


            string[] lines = File.ReadAllLines("textfile.txt");


            //textToSend = "TrackTurnLeft ";
            sw.WriteLine(lines[0]);
            sw.WriteLine(lines[1]);
            sw.WriteLine(lines[2]);
            sw.WriteLine(lines[3]);
            sw.WriteLine(lines[4]);
            sw.Flush();
            //---send the text---
            Console.WriteLine(lines);
            //nwStream.Write(bytesToSend, 0, bytesToSend.Length);
           
            /*
            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            
            */

            Console.ReadLine();
            client.Close();
            
        }

      
    }
}
