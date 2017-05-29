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
    class Player
    {
        private TcpClient player;
        private BinaryReader reader;
        private BinaryWriter writer;

        /// <summary>
        /// Connects to server.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public void ConnectToServer(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            player = new TcpClient();
            player.Connect(ep);

            NetworkStream stream = player.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }

        /// <summary>
        /// Writes the data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void WriteData(string data)
        {
            writer.Write(data);
        }

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <returns></returns>
        public string ReadData()
        {
            return reader.ReadString();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            player.Close();
        }


        /// <summary>
        /// Reads the and write to server.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string ReadAndWriteToServer(string data)
        {
            string result;

            WriteData(data);
            result = ReadData();

            return result;
        }
    }
}
