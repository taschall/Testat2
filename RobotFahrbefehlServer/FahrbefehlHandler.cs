using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace RobotFahrbefehlServer
{
    class FahrbefehlHandler
    {
        private TcpClient client;
        private int i = 0;  //Laufvariable für Fahrbefehl Array
        bool start = false;

        //private enum Fahrbefehl {
        //    TrackLine,
        //    TrackTurnLeft,
        //    TrackTurnRight,
        //    TrackArcLeft,
        //    TrackArcRight,
        //    Start,
        //    NumberOfFahrbefehle
        //};

        //public struct Fahrbefehle
        //{
        //    Fahrbefehl fahrbefehl;
        //    float valueL;
        //    int valueA;
        //}

        private ArrayList fahrbefehle = new ArrayList();  //Array um Fahrbefehle abzuspeichern

        public FahrbefehlHandler(TcpClient client)
        {
            this.client = client;
        }

        public void Do()
        {
            Console.WriteLine("Verbindung zu " +
                client.Client.RemoteEndPoint);

            try
            {
                string line;
                using (StreamReader sr = new StreamReader(client.GetStream()))
                {
                    while ((line = sr.ReadLine()) != null)
                    {

                        if (line.Contains("Start"))
                        {
                            start = true;
                            break;
                        }
                        else
                            fahrbefehle.Add(line);
                    }
                }
            }

            finally
            {
                client.Close();
            }


        }
    }
}
