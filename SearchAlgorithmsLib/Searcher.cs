using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        protected int evaluatedNodes;

        public Searcher()
        {
            evaluatedNodes = 0;
        }

        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;

            //throw new NotImplementedException();
        }

        protected Solution<T> BackTrace(State<T> state)
        {
            Solution<T> solution = new Solution<T>();
            solution.EvaluatedNodes = evaluatedNodes;
            solution.UpdateList(state);
            return solution;
        }
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
