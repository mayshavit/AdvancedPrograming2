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
using System.Windows.Shapes;
using System.Threading;

namespace ex2.View.MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SingleGameViewModel vm;

        public SinglePlayerWindow()
        {
            InitializeComponent();
            //this.DataContext = vm;
            vm = new SingleGameViewModel(new GameModel());
            this.DataContext = vm;
            this.KeyDown += mazeBoard.UserControl_KeyDown;
        }

        //public void CreateModel(string name, int rows, int cols)
        public void Update(string name, int rows, int cols)
        {
            vm.MazeName = name;
            vm.Rows = rows;
            vm.Cols = cols;
            vm.StartGame(false);

            //mazeBoard.DrawMaze();
            mazeBoard.DrawMaze(vm.Rows, vm.Cols, vm.Maze, vm.InitialPos, vm.GoalPos);
            //model = new GameModel(name, rows, cols);
            //vm = new SingleGameViewModel(new GameModel(name, rows, cols));

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            window.Close();
        }

        private void btnRestartGame_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateWindow())
            {
                mazeBoard.RestartGame();
            }
        }

        private bool ValidateWindow()
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure?", "Validation", System.Windows.Forms.MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateWindow())
            {
                MainWindow window = (MainWindow)Application.Current.MainWindow;
                window.Show();
                this.Hide();
            }
        }

        private void btnSolveMaze_Click(object sender, RoutedEventArgs e)
        {
            List<string> directions = vm.SolveGame();
            mazeBoard.RestartGame();
            Thread.Sleep(250);
            //System.Timers.Timer timer = new System.Timers.Timer(500);

            Thread thread = new Thread(PlayOneMove);

            thread.Start(directions);
           

        }

        private void PlayOneMove(Object obj)
        {
            List<string> directions = (List<string>)obj;


            for (int i = 0; i < directions.Count; i++)
            {
                mazeBoard.MovePlayer(directions[i]);
                //thread.Start(directions[i]);
                Thread.Sleep(250);
            }
            //mazeBoard.MovePlayer((string)obj);
        }

        /*private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e, string direction)
        {
            Controls.MazeBoard.MovePlayer(direction);
        }*/
    }
}
