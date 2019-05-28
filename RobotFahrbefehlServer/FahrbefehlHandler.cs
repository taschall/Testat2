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

        void ParseAndDrive()
        {
            String action;
            String value1;
            String value2 = null;

            //parse Arraylist fahrbefehle
            foreach(String element in fahrbefehle)
            {
                // get first fahrbefehl
                string s = element;

                // split string into sub strings
                string[] words = s.Split(' ');

                action = words[1];
                value1 = words[2];

                // Check if 3 substrings exists
                if(words.Length >= 2)
                {
                    value2 = words[3];
                }
                else
                {
                    value2 = null;
                }
                    

            //String s = dataReceived;
            //Char charRange = ' ';
            //int startIndex = s.IndexOf(charRange);
            //action = s.Substring(0, startIndex);
            //int endIndex = s.LastIndexOf(charRange);
            //if (endIndex == startIndex) { endIndex = s.Length; }
            //int length = endIndex - startIndex;
            //value1 = s.Substring(startIndex + 1, length - 1);
            //if (endIndex != s.Length)
            //{
            //    startIndex = endIndex;
            //    endIndex = s.Length;
            //    length = endIndex - startIndex;
            //    value2 = s.Substring(startIndex + 1, length - 1);
            //}


            float val1 = (float)System.Convert.ToDecimal(value1);
            int val2 = System.Convert.ToInt32(value2);

            //void printit()
            //{
            //    Console.WriteLine(action);
            //    Console.WriteLine("fValue1: " + val1); // Float
            //    Console.WriteLine("iValue2: " + val2); // INTEGER
            //}

            if (action.Contains("TrackLine"))
            {
                Console.WriteLine("TrackLine");
                //printit();
            }
            else if (action.Contains("TrackTurnLeft"))
            {
                Console.WriteLine("TrackTurnLeft");
                //printit();
            }
            else if (action.Contains("TrackTurnRight"))
            {
                Console.WriteLine("TrackTurnRight");
                //printit();
            }
            else if (action.Contains("TrackArcLeft"))
            {
                Console.WriteLine("TrackArcLeft");
                //printit();
            }
            else if (action.Contains("TrackArcRight"))
            {
                Console.WriteLine("TrackArcRight");
                //printit();
            }
            else if (action.Contains("Start"))
            {
                Console.WriteLine("Start");
                //printit();
            }

          }

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

                //finished receiving and start driving
                ParseAndDrive();

            }

            finally
            {
                client.Close();
            }


        }
    }
}
