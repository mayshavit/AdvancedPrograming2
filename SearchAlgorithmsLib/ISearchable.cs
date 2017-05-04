using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> GetInitialState();
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<T> GetGoalState();
        /// <summary>
        /// Gets all possible states according to the state.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}
