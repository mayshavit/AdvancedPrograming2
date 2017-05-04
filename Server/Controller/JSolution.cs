using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;


namespace Server
{
    public class JSolution
    {
        private string name;
        private string solution;
        private int nodesEvaluated;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public string Solution
        {
            get { return solution; }
            set { solution = value; }
        }

        /// <summary>
        /// Gets or sets the nodes evaluated.
        /// </summary>
        /// <value>
        /// The nodes evaluated.
        /// </value>
        public int NodesEvaluated
        {
            get { return nodesEvaluated; }
            set { nodesEvaluated = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JSolution"/> class.
        /// </summary>
        /// <param name="name2">The name2.</param>
        /// <param name="numOfNodes">The number of nodes.</param>
        public JSolution(string name2, int numOfNodes)
        {
            name = name2;
            nodesEvaluated = numOfNodes;
        }

        /// <summary>
        /// Sets the solution string.
        /// </summary>
        /// <param name="list">The list.</param>
        public void SetSolutionString (List<State<Position>> list)
        {
            for (int i = 0; i < list.Count-1; i++)
            {
                Position from = list.ElementAt(i).StateType;
                Position to = list.ElementAt(i + 1).StateType;

                Direction direction;

                if (from.Row == to.Row)
                {
                    if (from.Col > to.Col)
                    {
                        direction = Direction.Left;
                    }
                    else
                    {
                        direction = Direction.Right;
                    }
                }
                else
                {
                    if (from.Row > to.Row)
                    {
                        direction = Direction.Down;
                    }
                    else
                    {
                        direction = Direction.Up;
                    }
                }

                solution += (int)direction;
            }
        }

    }
}
