using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public interface ISearcher<T>
    {
        /// <summary>
        /// Searches the solution through the searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        int GetNumberOfNodesEvaluated();
    }
}
