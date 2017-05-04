using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    class PlayGameCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayGameCommand"/> class.
        /// </summary>
        /// <param name="model2">The model.</param>
        public PlayGameCommand (IModel model2)
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
            return model.PlayMove(args[0], client);
        }
    }
}
