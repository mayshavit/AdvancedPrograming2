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
        private SingleGameModel model;
        private Maze maze;
        private string name;
        private int rows;
        private int cols;
        private Position initialPos;
        private Position goalPos;

        public SingleGameViewModel(SingleGameModel model)
        {
            this.model = model;
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

        public void StartGame()
        {
            string json = model.GenerateMaze(name, rows, cols);
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
    }
}
