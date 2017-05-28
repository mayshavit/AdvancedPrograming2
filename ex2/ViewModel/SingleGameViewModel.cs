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

        public SingleGameViewModel(GameModel model)
        {
            this.model = model;
            this.model.OtherPlayerMoved += RaiseEvent;
        }

        public Maze Maze
        {
            get { return model.Maze; }
            set
            {
                //maze = value;
                model.Maze = value;
                maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                //name = value;
                model.MazeName = value;
                name = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public int Rows
        {
            get { return model.Rows; }
            set
            {
                //rows = value;
                model.Rows = value;
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public int Cols
        {
            get { return model.Cols; }
            set
            {
                //cols = value;
                model.Cols = value;
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        public Position InitialPos
        {
            get { return initialPos; }
            set { initialPos = value; }
        }

        public Position GoalPos
        {
            get { return goalPos; }
            set { goalPos = value; }
        }

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

            //maze = JsonConvert.DeserializeObject<Maze>(json);
            //Maze = Json.JsonParser.Deserialize(json);
            //Maze = JsonParser.Deserialize<Maze>(json);
            //Maze = JsonConvert.DeserializeObject(json);
            Maze = Maze.FromJSON(json);
            InitialPos = Maze.InitialPos;
            GoalPos = Maze.GoalPos;
        }

        /*public void Update(string name2, int rows2, int cols2)
        {
            //name = name2;
            rows = rows2;
            cols = cols2;
        }*/

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

        public void JoinGame()
        {
            string json = model.JoinGame();

            Maze maze = Maze.FromJSON(json); //JsonConvert.DeserializeObject<Maze>(json);

            Maze = maze;
            Rows = maze.Rows;
            Cols = maze.Cols;
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
        }

        public void Move(string move)
        {
            model.WriteMove(move);
        }

        private void RaiseEvent(object sender, PlayerMovedEventArgs e)
        {
            JMove jMove = JsonConvert.DeserializeObject<JMove>(e.Move);

            MoveOtherPlayer?.Invoke(this, new PlayerMovedEventArgs(jMove.Direction));
        }

        public void CloseGame()
        {

        }
    }
}
