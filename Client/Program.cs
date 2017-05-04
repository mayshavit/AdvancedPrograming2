using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;

//
namespace Client
{
    class Program
    {
        static private bool closeProgram = false;

        /// <summary>
        /// Writers the string to the server.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="client">The client.</param>
        static public void Writer (BinaryWriter writer, TcpClient client)
        {
            new Task(() =>
           {
               string s;
               s = Console.ReadLine();

               while (true)
               {
                   writer.Write(s);
                   if(s.StartsWith("close"))
                   {
                       break;
                   }
                   s = Console.ReadLine();
               }
               closeProgram = true;
           }).Start();
        }

        /// <summary>
        /// The Main function.
        /// Runs the program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpClient client = new TcpClient();
            client.Connect(ep);

            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            string s = Console.ReadLine();
            writer.Write(s);
            string s2 = reader.ReadString();
            Console.WriteLine(s2);

            if ((s.StartsWith("start"))||(s.StartsWith("join")))
            {
                Writer(writer, client);
                /*if (closeProgram)
                {
                    return;
                }*/

                while(!closeProgram)
                {
                    if (closeProgram)
                    {
                        break;
                    }

                    s2 = reader.ReadString();

                    if (s2 == "close")
                    {
                        break;
                    }
                    Console.WriteLine(s2);

                    if (s2 == "Command not found")
                    {
                        break;
                    }
                }
            }
            client.Close();
        }
    }
}
