using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;
using Json;

namespace ex2
{
    class SingleGameViewModel : ViewModel
    {
        private GameModel model;
        private Maze maze;
        private string name;
        private int rows;
        private int cols;
        private Position initialPos;
        private Position goalPos;

        public event EventHandler<PlayerMovedEventArgs> MoveOtherPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGameViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SingleGameViewModel(GameModel model)
        {
            this.model = model;
            this.model.OtherPlayerMoved += RaiseEvent;
        }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return model.Maze; }
            set
            {
                model.Maze = value;
                maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                name = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        /// <summary>
        /// Gets or sets the initial position.
        /// </summary>
        /// <value>
        /// The initial position.
        /// </value>
        public Position InitialPos
        {
            get { return initialPos; }
            set { initialPos = value; }
        }

        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>
        /// The goal position.
        /// </value>
        public Position GoalPos
        {
            get { return goalPos; }
            set { goalPos = value; }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="multi">if set to <c>true</c> [multi].</param>
        public void StartGame(bool multi)
        {
            string json;
            if (!multi)
            {
                json = model.GenerateMaze(name, rows, cols);
            }
            else
            {
                json = model.StartGame();
            }

            Maze = Maze.FromJSON(json);
            InitialPos = Maze.InitialPos;
            GoalPos = Maze.GoalPos;
        }


        /// <summary>
        /// Solves the game.
        /// </summary>
        /// <returns></returns>
        public List<string> SolveGame()
        {
            List<string> directionList = new List<string>();
            string json = model.SolveMaze();

            JSolution solution = JsonConvert.DeserializeObject<JSolution>(json);

            int length = solution.Solution.Length;

            for (int i = 0; i < length; i++)
            {
                directionList.Add(DirectionString(solution.Solution[i]));
            }

            return directionList;
        }

        /// <summary>
        /// Directions the string.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        private string DirectionString(char c)
        {
            switch (c)
            {
                case '0':
                    return "left";
                case '1':
                    return "right";
                case '2':
                    return "up";
                case '3':
                    return "down";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        public void JoinGame()
        {
            string json = model.JoinGame();

            Maze maze = Maze.FromJSON(json);

            Maze = maze;
            Rows = maze.Rows;
            Cols = maze.Cols;
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
        }

        /// <summary>
        /// Moves the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void Move(string move)
        {
            model.WriteMove(move);
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PlayerMovedEventArgs"/> 
        /// instance containing the event data.</param>
        private void RaiseEvent(object sender, PlayerMovedEventArgs e)
        {
            JMove jMove = JsonConvert.DeserializeObject<JMove>(e.Move);

            MoveOtherPlayer?.Invoke(this, new PlayerMovedEventArgs(jMove.Direction));
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            model.CloseGame();
        }
    }
}
