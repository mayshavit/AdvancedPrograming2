using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Windows;

namespace ex2
{
    class SingleGameModel
    {
        //private Maze maze;
        //private string name;
        //private int rows;
        //private int cols;
        private SinglePlayer player;

        public Maze Maze
        {
            get { return Properties.Settings.Default.Maze; }
            set { Properties.Settings.Default.Maze = value; }
        }

        public string MazeName
        {
            get { return Properties.Settings.Default.MazeName; }
            set{ Properties.Settings.Default.MazeName = value; }
        }

        public int Rows
        {
            get { return Properties.Settings.Default.Rows; }
            set { Properties.Settings.Default.Rows = value; }
        }

        public int Cols
        {
            get { return Properties.Settings.Default.Cols; }
            set { Properties.Settings.Default.Cols = value; }
        }


        /*public SingleGameModel(string name2, int rows2, int cols2)
        {
            name = name2;
            rows = rows2;
            cols = cols2;
            player = new SinglePlayer();
        }*/

        public SingleGameModel()
        {
            player = new SinglePlayer();
        }

        private void ManageConnection()
        {
            string ip = Properties.Settings.Default.ServerIP;
            int port = Properties.Settings.Default.ServerPort;

            player.ConnectToServer(ip, port);
        }

        //public void GenerateMaze()
        public string GenerateMaze(string name, int rows, int cols)
        {
            string data = "generate " + name + " " + rows.ToString() + " " + cols.ToString();

            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            //maze = JsonConvert.DeserializeObject<Maze>(json);
            //MazeName = maze.Name;
            return json;
        }

    }
}
