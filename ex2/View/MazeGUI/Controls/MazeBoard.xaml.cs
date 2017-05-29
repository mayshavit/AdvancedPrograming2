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
        private bool toMove;

        /// <summary>
        /// Occurs when [player moved].
        /// </summary>
        public event EventHandler<PlayerMovedEventArgs> PlayerMoved;

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public Position PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [to move].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [to move]; otherwise, <c>false</c>.
        /// </value>
        public bool ToMove
        {
            get { return toMove; }
            set { toMove = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeBoard"/> class.
        /// </summary>
        public MazeBoard()
        {
            InitializeComponent();
            toMove = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.
        //This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeBoard));

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.
        //This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard));

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.
        //This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.
        //This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeBoard));

        /// <summary>
        /// Draws the maze.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="maze">The maze.</param>
        /// <param name="initialPos">The initial position.</param>
        /// <param name="goalPos">The goal position.</param>
        public void DrawMaze(int rows, int cols, Maze maze, Position initialPos, Position goalPos)
        {
            Maze = maze;
            this.maze = maze;
            Rows = rows;
            this.rows = rows;
            Cols = cols;
            this.cols = cols;

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

                    UpdateRectangle(rectangle, Rows - i - 1, j, width, height);
                }
            }

            Rectangle initial = new Rectangle();
            ImageSource srcImage1 = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + 
                @"/../../../../Images/minion.jpg"));
            initial.Fill = new ImageBrush(srcImage1);
            UpdateRectangle(initial, Rows - initialPos.Row - 1, initialPos.Col, width, height);
            playerPos = initialPos;
            pRectangle = initial;

            Rectangle goal = new Rectangle();
            ImageSource srcImage2 = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + 
                @"/../../../../Images/banana.jpg"));
            goal.Fill = new ImageBrush(srcImage2);
            UpdateRectangle(goal, Rows - goalPos.Row - 1, goalPos.Col, width, height);
        }

        /// <summary>
        /// Updates the rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void UpdateRectangle(Rectangle rectangle, int i, int j, double width, double height)
        {
            rectangle.Width = width;
            rectangle.Height = height;
            Canvas.SetTop(rectangle, i + i * height);
            Canvas.SetLeft(rectangle, j + j * width);

            myCanvas.Children.Add(rectangle);
        }

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (toMove)
            {
                Key key = e.Key;
                string str = key.ToString().ToLower();

                MovePlayer(str);

                if (!toMove)
                {
                    PlayerWon();
                }

                PlayerMoved?.Invoke(this, new PlayerMovedEventArgs(str));
            }
        }

        /// <summary>
        /// Determines whether [is a wall] [the specified row and col].
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>
        ///   <c>true</c> if [is a wall] [the specified row and col]; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="str">The string.</param>
        public void MovePlayer(string str)
        {
            int col = playerPos.Col;
            int row = playerPos.Row;

            switch (str)
            {
                case "left":
                    col--;
                    break;

                case "right":
                    col++;
                    break;

                case "up":
                    row++;
                    break;

                case "down":
                    row--;
                    break;

                default:
                    break;
            }

            if (!IsAWall(row, col))
            {
                Position p = new Position();
                p.Row = row;
                p.Col = col;
                playerPos = p;
                row = rows - row - 1;

                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetTop(pRectangle, row + row * (450.00 / rows - 1));
                    Canvas.SetLeft(pRectangle, col + col * (450.00 / cols - 1));
                });

            }
            if (playerPos.Equals(maze.GoalPos))
            {
                toMove = false;
            }
        }

        /// <summary>
        /// Displayes a message of winning.
        /// </summary>
        public void PlayerWon()
        {
            MessageBox.Show("You Won!!!! :-)", "Winning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame()
        {
            playerPos = Maze.InitialPos;

            int row = this.rows - playerPos.Row - 1;
            int col = playerPos.Col;

            Canvas.SetTop(pRectangle, row + row * (450.00 / Rows - 1));
            Canvas.SetLeft(pRectangle, col + col * (450.00 / Cols - 1));
        }
    }
}
