using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace MazeTester
{
    class Program
    {
        static void CompareSolvers()
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(47, 39);
            //Maze copyMaze = new Maze();
            //copyMaze = maze;

            //CellType c = maze[5, 4];

            Console.WriteLine(maze.ToString());

            MazeAdapter<Position> adapter = new MazeAdapter<Position>(maze);

            ISearcher<Position> searcher;

            searcher = new BFS<Position>();

            Solution<Position> bfs = searcher.Search(adapter);

            searcher = new DFS<Position>();

            Solution<Position> dfs = searcher.Search(adapter);

            Console.WriteLine("BFS: {0}", bfs.EvaluatedNodes);
            Console.WriteLine("DFS: {0}", dfs.EvaluatedNodes);
        }
        static void Main(string[] args)
        {
            CompareSolvers();
        }
    }
}
