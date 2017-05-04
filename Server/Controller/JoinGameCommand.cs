using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    class JoinGameCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinGameCommand"/> class.
        /// </summary>
        /// <param name="model2">The model2.</param>
        public JoinGameCommand (IModel model2)
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
            Maze maze = model.JoinGame(args[0], client);
            return maze.ToJSON();
        }
    }
}
