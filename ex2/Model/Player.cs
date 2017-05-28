using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ex2
{
    abstract class Player
    {
        private TcpClient player;
        private BinaryReader reader;
        private BinaryWriter writer;

        public void ConnectToServer(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            player = new TcpClient();
            player.Connect(ep);

            NetworkStream stream = player.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }

        protected void WriteData(string data)
        {
            writer.Write(data);
        }

        protected string ReadData()
        {
            return reader.ReadString();
        }

        public void CloseConnection()
        {
            player.Close();
        }

        public abstract string ReadAndWriteToServer(string data);
    }
}
