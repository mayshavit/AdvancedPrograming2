using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;
//using System.Windows.Forms;

namespace ex2.View.MazeGUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        private Position playerPos;
        private Rectangle pRectangle;
        private int rows;
        private int cols;
        private Maze maze;

        delegate void move();
        event move PlayerMoved;

        public Position PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        /*    public Rectangle PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }*/

        public MazeBoard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }



        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeBoard));




        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard));




        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));



        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeBoard));



        //public void DrawMaze()
        public void DrawMaze(int rows, int cols, Maze maze, Position initialPos, Position goalPos)
        {
            Maze = maze;
            this.maze = maze;
            Rows = rows;
            this.rows = rows;
            Cols = cols;
            this.cols = cols;
            //int rows = Rows;
            //int cols = Cols;
            //Maze maze = Maze;

            double width = 450.00 / cols - 1;
            double height = 450.00 / rows - 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    if (maze[i, j] == CellType.Free)
                    {
                        rectangle.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    }
                    else if (maze[i, j] == CellType.Wall)
                    {
                        rectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }

                    /*if ((initialPos.Row == i) && (initialPos.Col == j))
                    {
                        
                    }*/

                    /*if ((goalPos.Row == i) && (goalPos.Col == j))
                    {
                    }*/

                    UpdateRectangle(rectangle, Rows - i - 1, j, width, height);
                }
            }

            Rectangle initial = new Rectangle();
            ImageSource srcImage1 = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/../../../../Images/minion.jpg"));
            initial.Fill = new ImageBrush(srcImage1);
            UpdateRectangle(initial, Rows - initialPos.Row - 1, initialPos.Col, width, height);
            playerPos = initialPos;
            pRectangle = initial;

            Rectangle goal = new Rectangle();
            ImageSource srcImage2 = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/../../../../Images/banana.jpg"));
            goal.Fill = new ImageBrush(srcImage2);
            UpdateRectangle(goal, Rows - goalPos.Row - 1, goalPos.Col, width, height);


        }

        private void UpdateRectangle(Rectangle rectangle, int i, int j, double width, double height)
        {

                    rectangle.Width = width;
                    rectangle.Height = height;
                    Canvas.SetTop(rectangle, i+i*height);
                    Canvas.SetLeft(rectangle, j+j*width);

                    myCanvas.Children.Add(rectangle);
        }
        public void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            string str = key.ToString().ToLower();

            MovePlayer(str);
        }

        private bool IsAWall(int row, int col)
        {
            if ((row < 0) || (col < 0) || (row > rows - 1) || (col > cols - 1))
            {
                return true;
            }
            if (maze[row, col] == CellType.Wall)
            {
                return true;
            }

            return false;
        }

        public void MovePlayer(string str)
        {
            int row = playerPos.Row; //(int)Canvas.GetTop(playerPos);
            int col = playerPos.Col; //(int)Canvas.GetLeft(playerPos);
            int topRow = row;

            switch (str)
            {
                case "left":
                    col--;
                    break;

                case "right":
                    col++;
                    break;

                case "up":
                    row--;
                    topRow++;
                    break;

                case "down":
                    row++;
                    topRow--;
                    break;

                default:
                    break;
            }

            if (!IsAWall(topRow, col))
            {
                Position p = new Position();
                p.Row = topRow; //row;
                p.Col = col;
                playerPos = p;
                //row = Rows - row - 1;
                topRow = rows - topRow - 1;

                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetTop(pRectangle, topRow + topRow * (450.00 / rows - 1));
                    Canvas.SetLeft(pRectangle, col + col * (450.00 / cols - 1));
                });

            }

            if (playerPos.Equals(maze.GoalPos))
            {
                //System.Windows.Forms.MessageBox mb;
                PlayerWon();
            }
        }

        public void PlayerWon()
        {
            MessageBox.Show("You Won!!!! :-)", "Winning", MessageBoxButton.OK);
        }

        public void RestartGame()
        {
            playerPos = Maze.InitialPos;

            //int row = Rows - playerPos.Row - 1;
            int row = this.rows - playerPos.Row - 1;
            int col = playerPos.Col;

            Canvas.SetTop(pRectangle, row + row * (450.00 / Rows - 1));
            Canvas.SetLeft(pRectangle, col + col * (450.00 / Cols - 1));
        }
    }
}
