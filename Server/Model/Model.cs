using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json;

namespace Server
{
    class Model : IModel
    {
        private Controller controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="controller2">The controller.</param>
        public Model (Controller controller2)
        {
            controller = controller2;
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();

            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            controller.AddSingleGame(name, new SingleGame(name, maze, null));

            //controller.AddMaze(name, maze);
            return maze;
        }

        /// <summary>
        /// Returns a list of the mazes' names.
        /// </summary>
        /// <returns></returns>
        public List<string> MazesNames()
        {
            return controller.GetMazesNames();
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        public Solution<Position> SolveMaze(string name, int algorithm)
        {
            SingleGame game = controller.GetSingleGame(name);

            if (game != null)
            {
                //Solution<Position> solution = controller.GetSolution(name);
                Solution<Position> solution = game.Solution;
                if (!(solution == null))
                {
                    return solution;
                }
                //Maze maze = controller.GetMaze(name);
                Maze maze = game.Maze;
                ISearcher<Position> searcher = null;

                if (algorithm == 0)
                {
                    searcher = new BFS<Position>();
                }
                else if (algorithm == 1)
                {
                    searcher = new DFS<Position>();
                }

                MazeAdapter<Position> mazeAdapter = new MazeAdapter<Position>(maze);
                solution = searcher.Search(mazeAdapter);
                //controller.AddSolution(name, solution);
                game.Solution = solution;
                return solution;
            }
            return null;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        public Maze StartGame(string name, int rows, int cols, ClientNotifier gamer)
        {
            //Maze maze = GenerateMaze(name, rows, cols);
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            MultiGame game = new MultiGame(name, maze, null);
            game.AddGamer(gamer);
            //controller.AddGame(name, game);
            controller.AddMultiGame(name, game);
            return maze;
        }

        /// <summary>
        /// Connect a gamer to the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        public Maze JoinGame(string name, ClientNotifier gamer)
        {
            //MultiGame game = controller.GetGame(name);
            MultiGame game = controller.GetMultiGame(name);
            game.AddGamer(gamer);
            game.NotifyGamers();

            Maze maze = game.Maze;
            return maze;
        }

        /// <summary>
        /// Plays a move in the maze according to the gamer.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        public string PlayMove(string move, ClientNotifier gamer)
        {
            MultiGame game = controller.GetGameByGamer(gamer);
            JMove jMove = new JMove(game.Name, move);
            string json = JsonConvert.SerializeObject(jMove);
            game.UpdateMove(json, gamer);

            return json;
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        public string CloseGame(string name, ClientNotifier gamer)
        {
            //MultiGame game = controller.GetGame(name);
            MultiGame game = controller.GetMultiGame(name);
            game.CloseGame();

            //controller.RemoveGame(name);
            controller.RemoveMultiGame(name);

            return "close";
        }
    }
}
