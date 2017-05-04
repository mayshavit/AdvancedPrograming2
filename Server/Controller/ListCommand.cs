using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    class ListCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommand"/> class.
        /// </summary>
        /// <param name="model2">The model.</param>
        public ListCommand (IModel model2)
        {
            model = model2;
        }
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="args">The command.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, ClientNotifier client)
        {
            client.ToSend = true;
            client.ChangeToClose = true;
            return JsonConvert.SerializeObject(model.MazesNames());
        }
    }
}
