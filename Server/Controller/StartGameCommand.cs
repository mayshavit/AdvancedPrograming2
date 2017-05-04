using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    class StartGameCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameCommand"/> class.
        /// </summary>
        /// <param name="model2">The model2.</param>
        public StartGameCommand(IModel model2)
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
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = model.StartGame(name, rows, cols, client);

            return maze.ToJSON();
        }
    }
}
