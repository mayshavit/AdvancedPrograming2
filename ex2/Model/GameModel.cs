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
    class GameModel
    {
        //private Maze maze;
        //private string name;
        //private int rows;
        //private int cols;
        private SinglePlayer player1;
        private SinglePlayer player2;

        public event EventHandler<PlayerMovedEventArgs> OtherPlayerMoved;

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


        /*public GameModel(string name2, int rows2, int cols2)
        {
            name = name2;
            rows = rows2;
            cols = cols2;
            player = new SinglePlayer();
        }*/

        public GameModel()
        {
            player1 = new SinglePlayer();
            player2 = new SinglePlayer();
            //closeConnection = false;
        }

        private void ManageConnection()
        {
            string ip = Properties.Settings.Default.ServerIP;
            int port = Properties.Settings.Default.ServerPort;

            player1.ConnectToServer(ip, port);
        }

        //public void GenerateMaze()
        public string GenerateMaze(string name, int rows, int cols)
        {
            string data = "generate " + name + " " + rows.ToString() + " " + cols.ToString();

            ManageConnection();

            string json = player1.ReadAndWriteToServer(data);
            //maze = JsonConvert.DeserializeObject<Maze>(json);
            //MazeName = maze.Name;
            player1.CloseConnection();
            return json;
        }

        public string SolveMaze()
        {
            string data = "solve " + MazeName + " " + Properties.Settings.Default.SearchAlgorithm.ToString();

            ManageConnection();

            string json = player1.ReadAndWriteToServer(data);
            player1.CloseConnection();
            return json;
        }

        public string ListOfMazes()
        {
            string data = "list";

            ManageConnection();

            string json = player1.ReadAndWriteToServer(data);
            player1.CloseConnection();

            return json;
        }

        public string StartGame()
        {
            string data = "start " + MazeName + " " + Rows.ToString() + " " + Cols.ToString();

            ManageConnection();

            string json = player1.ReadAndWriteToServer(data);

            this.ReadMessages();

            return json;
        }

        public string JoinGame()
        {
            string data = "join " + MazeName;

            ManageConnection();

            string json = player1.ReadAndWriteToServer(data);

            this.ReadMessages();

            return json;
        }

        public void WriteMove(string move)
        {
            string data = "play " + move;

            player1.WriteData(data);
        }

        public void ReadMessages()
        {
            new Task(() =>
            {
                string s;

                while (true)
                {
                    s = player1.ReadData();
                    if (s == "close")
                    {
                        break;
                    }
                    OtherPlayerMoved?.Invoke(this, new PlayerMovedEventArgs(s));
                }
            }).Start();
        }

        public void CloseGame()
        {
            string data = "close " + MazeName;

            player1.WriteData(data);
        }
    }
}
