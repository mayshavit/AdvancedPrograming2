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

        public GameName(string name)
        {
            this.name = name;
        }

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

        public PlayerConnectedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }

    public class PlayerMovedEventArgs : EventArgs
    {
        public string Move { get; set; }

        public PlayerMovedEventArgs(string move)
        {
            Move = move;
        }
    }
}
