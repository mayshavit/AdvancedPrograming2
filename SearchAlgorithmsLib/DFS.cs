using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFS<T> : SearcherWithStack<T>
    {
        //private Stack<State<T>> stack;
        //private int evaluatedNodes;

        /*/// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        public DFS()
        {
            stack = new Stack<State<T>>();
            evaluatedNodes = 0;
        }*/

        /*/// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }*/

        /// <summary>
        /// Searches the solution through the searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> goalState = searchable.GetGoalState();
            State<T> initialState = searchable.GetInitialState();
            //initialState.Color = 1;
            initialState.Cost = 1;
            //stack.Push(initialState);
            AddToStack(initialState);

            //while (stack.Count > 0)
            while (StackSize > 0)
            {
                //State<T> n = stack.Pop();
                //evaluatedNodes++;

                State<T> state = PopStack();

                //if (n.Equals(goalState))
                if (state.Equals(goalState))
                {
                    return BackTrace(state);
                    
                    //return BackTrace(state, initialState);
                    //return BackTrace(n, initialState);
                }

                //List<State<T>> succerssors = searchable.GetAllPossibleStates(n);

                List<State<T>> succerssors = searchable.GetAllPossibleStates(state);

                foreach (State<T> s in succerssors)
                {
                    if (s.Color == 0)
                    //if (s.Cost==0)
                    {
                        //s.Cost = n.Cost + 1;
                        //s.CameFrom = n;
                        s.CameFrom = state;
                        //stack.Push(s);
                        AddToStack(s);
                    }
                    s.Color = 1;
                    //s.Cost = 1;
                }
            }
            return null;
        }

        /*/// <summary>
        /// Return a solution according to the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="initialState">The initial state.</param>
        /// <returns></returns>
        public Solution<T> BackTrace(State<T> state, State<T> initialState)
        {
            Solution<T> solution = new Solution<T>();
            solution.EvaluatedNodes = evaluatedNodes;
            //solution.UpdateList(state, initialState);
            solution.UpdateList(state);
            return solution;
        }*/
    }
}
