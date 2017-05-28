using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;


namespace ex2
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
    }
}
