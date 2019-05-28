using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using Baymax.Control;
using System.Threading;

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
        private ArrayList Encoder = new ArrayList();
        //private ArrayList EncoderRight = new ArrayList();
        private Robot robot;

        public FahrbefehlHandler(TcpClient client)
        {
            this.client = client;
            robot = new Robot();
        }

        void ParseAndDrive()
        {
            String action;
            String value1;
            String value2 = null;

            // enable driving
            robot.Drive.Power = true;

            //parse Arraylist fahrbefehle
            foreach (String element in fahrbefehle)
            {
                // get first fahrbefehl
                string s = element;

                // split string into sub strings
                string[] words = s.Split(' ');

                action = words[0];
                value1 = words[1];

                // Check if 3 substrings exists
                if (words.Length >= 3)
                {
                    value2 = words[2];
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
                    // gerade aus fahren
                    robot.Drive.RunLine(val1, 0.3f, 0.5f);
                    //printit();
                }
                else if (action.Contains("TrackTurnLeft"))
                {
                    Console.WriteLine("TrackTurnLeft");
                    robot.Drive.RunTurn(-val1, 0.3f, 0.5f);
                    //printit();
                }
                else if (action.Contains("TrackTurnRight"))
                {
                    Console.WriteLine("TrackTurnRight");
                    robot.Drive.RunTurn(val1, 0.3f, 0.5f);
                    //printit();
                }
                else if (action.Contains("TrackArcLeft"))
                {
                    Console.WriteLine("TrackArcLeft");
                    robot.Drive.RunArcRight(val2, val1, 0.3f, 0.5f);
                    //printit();
                }
                else if (action.Contains("TrackArcRight"))
                {
                    Console.WriteLine("TrackArcRight");
                    robot.Drive.RunArcRight(val2, -val1, 0.3f, 0.5f);
                    //printit();
                }
                else if (action.Contains("Start"))
                {
                    Console.WriteLine("Start");
                    //printit();
                }

                //warten bis fertig gefahren
                while (!robot.Drive.Done)
                {
                    Thread.Sleep(200);
                    Encoder.Add(robot.Drive.DriveInfo.DistanceL + "," + robot.Drive.DriveInfo.DistanceR);

                }
            }

            // Roboter stoppen
            robot.Drive.Stop();

            // write array with encoder data to file
            WriteFile();
        }

        public void WriteFile()
        {
            FileStream fs = new FileStream("daten.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach(float encoder in Encoder)
            {
                sw.WriteLine(encoder);
            }
            sw.Close();
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
                    Console.WriteLine("Starten mit empfangen Fahrbefehle");

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Empfange: " + line);

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
