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
        private Player player;
        private bool connectionClosed;

        /// <summary>
        /// Occurs when [other player moved].
        /// </summary>
        public event EventHandler<PlayerMovedEventArgs> OtherPlayerMoved;

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return Properties.Settings.Default.Maze; }
            set { Properties.Settings.Default.Maze = value; }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return Properties.Settings.Default.MazeName; }
            set { Properties.Settings.Default.MazeName = value; }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows
        {
            get { return Properties.Settings.Default.Rows; }
            set { Properties.Settings.Default.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols
        {
            get { return Properties.Settings.Default.Cols; }
            set { Properties.Settings.Default.Cols = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
            player = new Player();
            connectionClosed = false;
        }

        /// <summary>
        /// Manages the connection.
        /// </summary>
        private void ManageConnection()
        {
            string ip = Properties.Settings.Default.ServerIP;
            int port = Properties.Settings.Default.ServerPort;

            player.ConnectToServer(ip, port);
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public string GenerateMaze(string name, int rows, int cols)
        {
            string data = "generate " + name + " " + rows.ToString() + " " + cols.ToString();
            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            player.CloseConnection();
            return json;
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <returns></returns>
        public string SolveMaze()
        {
            string data = "solve " + MazeName + " " + Properties.Settings.Default.SearchAlgorithm.ToString();
            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            player.CloseConnection();
            return json;
        }

        /// <summary>
        /// Returns a list of the mazes.
        /// </summary>
        /// <returns></returns>
        public string ListOfMazes()
        {
            string data = "list";
            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            player.CloseConnection();
            return json;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <returns></returns>
        public string StartGame()
        {
            string data = "start " + MazeName + " " + Rows.ToString() + " " + Cols.ToString();
            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            this.ReadMessages();
            return json;
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <returns></returns>
        public string JoinGame()
        {
            string data = "join " + MazeName;
            ManageConnection();

            string json = player.ReadAndWriteToServer(data);
            this.ReadMessages();
            return json;
        }

        /// <summary>
        /// Writes the move to server.
        /// </summary>
        /// <param name="move">The move.</param>
        public void WriteMove(string move)
        {
            string data = "play " + move;
            player.WriteData(data);
        }

        /// <summary>
        /// Reads the messages in a new thread.
        /// </summary>
        public void ReadMessages()
        {
            new Task(() =>
            {
                string s;
                while (true)
                {
                    s = player.ReadData();
                    if (s == "close")
                    {
                        player.CloseConnection();
                        break;
                    }
                    OtherPlayerMoved?.Invoke(this, new PlayerMovedEventArgs(s));
                }
            }).Start();
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            if (!connectionClosed)
            {
                string data = "close " + MazeName;
                player.WriteData(data);
                connectionClosed = true;
            }
        }
    }
}
