using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server
{
    class TcpServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServer"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public TcpServer (int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();

            Task task = new Task(() =>
           {
               while (true)
               {
                   try
                   {
                       TcpClient client = listener.AcceptTcpClient();
                       ch.HandleClient(client);
                   }
                   catch (SocketException)
                   {
                       break;
                   }
               }
           });
           task.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}
