using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    public class GameName
    {
        string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameName"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public GameName(string name)
        {
            this.name = name;
        }

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
    }

    class GameNames : List<GameName>
    {
    }

    public class PlayerConnectedEventArgs : EventArgs
    {
        public bool Connected { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="connected">if set to <c>true</c> [connected].</param>
        public PlayerConnectedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }

    public class PlayerMovedEventArgs : EventArgs
    {
        public string Move { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMovedEventArgs"/> class.
        /// </summary>
        /// <param name="move">The move.</param>
        public PlayerMovedEventArgs(string move)
        {
            Move = move;
        }
    }
}
