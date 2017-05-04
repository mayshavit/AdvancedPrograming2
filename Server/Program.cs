using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            TcpServer server = new TcpServer(port, new ClientHandler());
            server.Start();
            while (true)
            {
                string s = "7";
            }
        }
    }
}
