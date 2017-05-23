using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using System.Net;

namespace ex2
{
    class SingleGameModel : ViewModel
    {
        private Maze maze;
        private string name;
        private int rows;
        private int cols;

        public SingleGameModel(string name2, int rows2, int cols2)
        {
            name = name2;
            rows = rows2;
            cols = cols2;
        }

        private void ManageConnection()
        {
            string ip = Properties.Settings.Default.ServerIP;
            int port = Properties.Settings.Default.ServerPort;
        }
    }
}
