using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server
{
    interface IModel
    {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        Maze GenerateMaze(string name, int rows, int cols);
        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        Solution<Position> SolveMaze(string name, int algorithm);
        /// <summary>
        /// Returns a list of the mazes' names.
        /// </summary>
        /// <returns></returns>
        List<string> MazesNames();
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        Maze StartGame(string name, int rows, int cols, ClientNotifier gamer);
        /// <summary>
        /// Connect a gamer to the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        Maze JoinGame(string name, ClientNotifier gamer);
        /// <summary>
        /// Plays a move in the maze according to the gamer.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        string PlayMove(string move, ClientNotifier gamer);
        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gamer">The gamer.</param>
        /// <returns></returns>
        string CloseGame(string name, ClientNotifier gamer);
    }
}
