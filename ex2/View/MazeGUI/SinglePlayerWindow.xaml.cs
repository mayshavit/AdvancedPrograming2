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

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindow"/> class.
        /// </summary>
        public SinglePlayerWindow()
        {
            InitializeComponent();
            vm = new SingleGameViewModel(new GameModel());
            this.DataContext = vm;
            this.KeyDown += mazeBoard.UserControl_KeyDown;
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void Update(string name, int rows, int cols)
        {
            vm.MazeName = name;
            vm.Rows = rows;
            vm.Cols = cols;
            vm.StartGame(false);

            mazeBoard.DrawMaze(vm.Rows, vm.Cols, vm.Maze, vm.InitialPos, vm.GoalPos);
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> 
        /// instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            window.Close();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnRestartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRestartGame_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateWindow())
            {
                mazeBoard.ToMove = true;
                mazeBoard.RestartGame();
            }
        }

        /// <summary>
        /// Validates the window.
        /// </summary>
        /// <returns></returns>
        private bool ValidateWindow()
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure?", 
                "Validation", System.Windows.Forms.MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handles the Click event of the btnMainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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

            Thread thread = new Thread(PlayOneMove);

            thread.Start(directions);


        }

        /// <summary>
        /// Plays the one move.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void PlayOneMove(Object obj)
        {
            List<string> directions = (List<string>)obj;


            for (int i = 0; i < directions.Count; i++)
            {
                mazeBoard.MovePlayer(directions[i]);
                Thread.Sleep(250);
            }
        }
    }
}
