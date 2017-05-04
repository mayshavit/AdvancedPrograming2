using System;
using System.Collections.Generic;
using System.Text;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private string mazeName;
        private List<State<T>> solution;
        private int nodesEvaluated;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return mazeName; }
            set { mazeName = value; }
        }

        /// <summary>
        /// Gets or sets the evaluated nodes.
        /// </summary>
        /// <value>
        /// The evaluated nodes.
        /// </value>
        public int EvaluatedNodes
        {
            get { return nodesEvaluated; }
            set { nodesEvaluated = value; }
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>
        /// The list.
        /// </value>
        public List<State<T>> List
        {
            get { return solution; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        public Solution ()
        {
            solution = new List<State<T>>();
            nodesEvaluated = 0;
        }

        /// <summary>
        /// Updates the list.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="initialState">The initial state.</param>
        //public void UpdateList (State<T> state, State<T> initialState)
        public void UpdateList (State<T> state)
        {
            //while(!state.Equals(initialState))
            while(state.CameFrom!=null)
            {
                solution.Add(state);
                state = state.CameFrom;
            }

            solution.Add(state);
            solution.Reverse();
        }
    }
}
