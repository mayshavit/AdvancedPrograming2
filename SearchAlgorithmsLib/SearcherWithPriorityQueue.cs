using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class SearcherWithPriorityQueue<T> : Searcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        //private int evaluatedNodes;

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        {
            get { return openList.Count; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public SearcherWithPriorityQueue()
        {
            openList = new SimplePriorityQueue<State<T>>();
            //evaluatedNodes = 0;
        }

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }


        /*/// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }*/

        /// <summary>
        /// Adds a state to open list.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="priority">The priority.</param>
        public void AddToOpenList (State<T> state, float priority)
        {
            openList.Enqueue(state, priority);
        }

        /// <summary>
        /// Checks if a state is in the openList.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool OpenContains (State<T> s)
        {
            return openList.Contains(s);
        }

        /// <summary>
        /// Adjusts the priority.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="priority">The priority.</param>
        public void AdjustPriority (State<T> state, float priority)
        {
            foreach (State<T> s in openList)
            {
                if(s.Equals(state))
                {
                    openList.Remove(state);
                    openList.Enqueue(state, priority);
                    return;
                }
            }
        }
        /// <summary>
        /// Searches the solution through the searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        //public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
