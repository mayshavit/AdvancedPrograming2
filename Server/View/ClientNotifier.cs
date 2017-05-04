using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    public class ClientNotifier
    {
        private TcpClient client;
        bool toClose;
        bool toSend;
        bool changeToClose;
        private string data;

        /// <summary>
        /// Gets or sets a value indicating whether to close the connectin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [to close]; otherwise, <c>false</c>.
        /// </value>
        public bool ToClose
        {
            get { return toClose; }
            set { toClose = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to send the data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [to send]; otherwise, <c>false</c>.
        /// </value>
        public bool ToSend
        {
            get { return toSend; }
            set { toSend = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether change the bool to close.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change to close]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeToClose
        {
            get { return changeToClose; }
            set { changeToClose = value; }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientNotifier"/> class.
        /// </summary>
        /// <param name="client2">The client.</param>
        public ClientNotifier (TcpClient client2)
        {
            client = client2;
            toClose = false;
            toSend = false;
            changeToClose = false;
        }

        /// <summary>
        /// Notifies the result.
        /// </summary>
        public void NotifyResult ()
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(data);

            if (changeToClose)
            {
                toClose = true;
            }
        }
    }
}
