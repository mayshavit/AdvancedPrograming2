using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CloseGameCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseGameCommand"/> class.
        /// </summary>
        /// <param name="model2">The model2.</param>
        public CloseGameCommand(IModel model2)
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
            return model.CloseGame(args[0], client);
        }
    }
}
