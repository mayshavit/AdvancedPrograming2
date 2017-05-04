using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state;
        private double cost;
        private State<T> cameFrom;
        private int color;

        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>
        /// The came from.
        /// </value>
        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /// <summary>
        /// Gets the type of the state.
        /// </summary>
        /// <value>
        /// The type of the state.
        /// </value>
        public T StateType
        {
            get { return state; }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public int Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        private State(T state)
        {
            this.state = state;
            cost = 0;
            color = 0;
        }

        /// <summary>
        /// Checks if two states are equal.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        public static class StatePool
        {
            private static HashSet<T> pool = new HashSet<T>();
            private static Dictionary<T, State<T>> states = new Dictionary<T, State<T>>();

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="state">The state.</param>
            /// <returns></returns>
            public static State<T> GetState(T state)
            {
                if (pool.Contains(state))
                {
                    return states[state];
                }
                else
                {
                    State<T> s = new State<T>(state);
                    pool.Add(state);
                    states.Add(state, s);
                    return states[state];
                }
            }
        }
    }
}
