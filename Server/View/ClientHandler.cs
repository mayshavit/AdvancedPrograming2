using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    public class ClientHandler : IClientHandler
    {
        Controller controller;
        private List<ClientNotifier> notifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        public ClientHandler()
        {
            controller = new Controller();
            notifiers = new List<ClientNotifier>();
        }

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient (TcpClient client)
        {
           new Task(() =>
           {
               ClientNotifier notifier = new ClientNotifier(client);
               notifiers.Add(notifier);

               NetworkStream stream = client.GetStream();
               BinaryReader reader = new BinaryReader(stream);
               BinaryWriter writer = new BinaryWriter(stream);

               while (!notifier.ToClose)
               {
                   string commandLine = reader.ReadString();
                   string result = controller.ExecuteCommand(commandLine, notifier);
                   notifier.Data = result;
                   NotifyAll();
               }
               client.Close();
               notifiers.Remove(notifier);

           }).Start();
        }

        /// <summary>
        /// Notifies all clients if they should recive data.
        /// </summary>
        public void NotifyAll ()
        {
            for (int i = 0; i < notifiers.Count ; i++)
            {
                if (notifiers.ElementAt(i).ToSend)
                {
                    notifiers.ElementAt(i).NotifyResult();
                }
            }
        }
    }
}
