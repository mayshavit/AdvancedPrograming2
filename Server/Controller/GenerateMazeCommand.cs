using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;

namespace Server
{
    class GenerateMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
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

            Maze maze = model.GenerateMaze(name, rows, cols);
            client.ToSend = true;
            client.ChangeToClose = true;
            //return maze.ToJSON();
            return maze.ToJSON();
            //return JsonConvert.SerializeObject(maze);
            //return maze.ToString();
        }
    }
}
