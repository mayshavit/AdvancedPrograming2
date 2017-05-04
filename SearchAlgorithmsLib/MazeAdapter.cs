using System;
using System.Collections.Generic;
using System.Text;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class MazeAdapter<T> : ISearchable<Position>
    {
        Maze maze;
        State<Position> initialState;
        State<Position> goalState;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter{T}"/> class.
        /// </summary>
        /// <param name="maze2">The maze.</param>
        public MazeAdapter (Maze maze2)
        {
            maze = maze2;
            initialState = State<Position>.StatePool.GetState(maze.InitialPos);
            goalState = State<Position>.StatePool.GetState(maze.GoalPos);
        }

        /// <summary>
        /// Gets all possible states according to the state.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> possibleStates = new List<State<Position>>();
            List<Position> positions = GetNeighbours(s.StateType);

            foreach (Position p in positions)
            {
                //????????????????????????????????//
                if (p.Equals(maze.InitialPos))
                {
                    continue;
                }
                //????????????????????????????????//
                if ((p.Row >= 0) && (p.Col >= 0) && (p.Row < maze.Rows) && (p.Col < maze.Cols))
                {
                    if (maze[p.Row, p.Col] == CellType.Free)
                    {
                        State<Position> state = State<Position>.StatePool.GetState(p);
                        possibleStates.Add(state);
                    }
                }
            }
            return possibleStates;
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetGoalState()
        {
            return goalState;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState()
        {
            return initialState;
        }

        public List<Position> GetNeighbours (Position position)
        {
            int row = position.Row;
            int col = position.Col;

            Position left = new Position();
            Position up = new Position();
            Position right = new Position();

            Position down = new Position();

            left.Row = row;
            left.Col = col - 1;

            up.Row = row + 1;
            up.Col = col;

            right.Row = row;
            right.Col = col + 1;

            down.Row = row - 1;
            down.Col = col;

            List<Position> positions = new List<Position>();

            positions.Add(left);
            positions.Add(up);
            positions.Add(right);            
            positions.Add(down);

            return positions;
        }
    }
}
