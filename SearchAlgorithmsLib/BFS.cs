using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : SearcherWithPriorityQueue<T>
    {
        /// <summary>
        /// Searches the solution through the searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> state = searchable.GetInitialState();
            AddToOpenList(searchable.GetInitialState(),0);
            HashSet<State<T>> closed = new HashSet<State<T>>();

            while(OpenListSize>0)
            {
                State<T> n = PopOpenList();
                closed.Add(n);

                if (n.Equals(searchable.GetGoalState()))
                {
                    return BackTrace(n);
                    
                    //return BackTrace(n, searchable.GetInitialState());
                }

                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);

                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !OpenContains(s))
                    {
                        s.CameFrom = n;
                        //double cost = s.Cost;
                        //s.Cost = ++cost;
                        s.Cost++;
                        //AddToOpenList(s, 1);
                        AddToOpenList(s, (float)s.Cost);
                    }
                    //else if (s.Cost-n.Cost==1)
                    else if (s.Cost > n.Cost + 1)
                    {
                        if (!OpenContains(s))
                        {
                            s.CameFrom = n;
                            s.Cost = n.Cost + 1;
                            AddToOpenList(s, (float)s.Cost);
                            //AddToOpenList(s,1);
                        }
                        else
                        {
                            AdjustPriority(s, (float)s.Cost);
                        }
                    }
                }
            }
            return null;
        }

        /*/// <summary>
        /// Returns a solution according to the state
        /// </summary>x
        /// <param name="state">The state.</param>
        /// <param name="initialState">The initial state.</param>
        /// <returns></returns>
        private Solution<T> BackTrace (State<T> state, State<T> initialState)
        {
            Solution<T> solution = new Solution<T>();
            solution.EvaluatedNodes = GetNumberOfNodesEvaluated();
            //solution.UpdateList(state, initialState);
            solution.UpdateList(state);
            return solution;
        }*/

    }
}
