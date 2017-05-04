using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
    class SolveMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand (IModel model)
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
            int algorithm = int.Parse(args[1]);

            client.ToSend = true;
            client.ChangeToClose = true;

            Solution<Position> solution = model.SolveMaze(name, algorithm);
            JSolution jSolution = new JSolution(name, solution.EvaluatedNodes);
            jSolution.SetSolutionString(solution.List);

            return JsonConvert.SerializeObject(jSolution);
        }
    }
}
