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
        public void DrawMaze(int rows, int cols, Maze maze)
        {
            //int rows = Rows;
            //int cols = Cols;
            //Maze maze = Maze;

            double width = 370 / cols;
            double height = 370 / rows;
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

                    rectangle.Width = width;
                    rectangle.Height = height;
                    Canvas.SetTop(rectangle, i+i*width);
                    Canvas.SetLeft(rectangle, j+j*height);

                    myCanvas.Children.Add(rectangle);
                }

            }
        }

    }
}
