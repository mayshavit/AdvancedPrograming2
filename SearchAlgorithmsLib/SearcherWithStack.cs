using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class SearcherWithStack<T> : Searcher<T>
    {
        protected Stack<State<T>> stack;

        public int StackSize
        {
            get { return stack.Count; }
        }

        public SearcherWithStack()
        {
            stack = new Stack<State<T>>();
        }

        protected State<T> PopStack()
        {
            evaluatedNodes++;
            return stack.Pop();
        }

        public void AddToStack(State<T> state)
        {
            stack.Push(state);
        }

        public bool StackContains(State<T> state)
        {
            return stack.Contains(state);
        }


    }
}
