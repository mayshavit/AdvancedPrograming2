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

namespace ex2.View.MazeGUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private SingleGameViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindow"/> class.
        /// </summary>
        public MultiPlayerWindow()
        {
            InitializeComponent();
            vm = new SingleGameViewModel(new GameModel());
            this.DataContext = vm;
            this.KeyDown += myBoard.UserControl_KeyDown;
            myBoard.PlayerMoved += WriteMoveToServer;
            vm.MoveOtherPlayer += MoveOtherPlayer;
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing 
        /// the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseGame();
            Window window = Application.Current.MainWindow;
            window.Close();
            this.Hide();
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        public void Update(string name, int rows, int cols, bool start)
        {
            vm.MazeName = name;
            vm.Rows = rows;
            vm.Cols = cols;

            if (start)
            {
                vm.StartGame(true);
            }
            else
            {
                vm.JoinGame();
            }

            myBoard.DrawMaze(vm.Rows, vm.Cols, vm.Maze, vm.InitialPos, vm.GoalPos);
            otherBoard.DrawMaze(vm.Rows, vm.Cols, vm.Maze, vm.InitialPos, vm.GoalPos);
        }

        /// <summary>
        /// Writes the move to server.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PlayerMovedEventArgs"/> instance containing the event data.</param>
        private void WriteMoveToServer(object sender, PlayerMovedEventArgs e)
        {
            vm.Move(e.Move);

            if (!myBoard.ToMove)
            {
                vm.CloseGame();
                otherBoard.ToMove = false;
            }
        }

        /// <summary>
        /// Moves the other player.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PlayerMovedEventArgs"/> instance containing the event data.</param>
        private void MoveOtherPlayer(object sender, PlayerMovedEventArgs e)
        {
            otherBoard.MovePlayer(e.Move);

            if (!otherBoard.ToMove)
            {
                vm.CloseGame();
                otherBoard.ToMove = false;
                myBoard.ToMove = false;
                System.Windows.Forms.MessageBox.Show("You Lost!!!! :-(", "Losing", 
                    System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CloseGame();
            Window window = Application.Current.MainWindow;
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        private void CloseGame()
        {
            vm.CloseGame();
        }
    }
}
