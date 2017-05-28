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
        public event EventHandler<PlayerConnectedEventArgs> PlayerConnected;

        public MultiPlayerWindow()
        {
            InitializeComponent();
            vm = new SingleGameViewModel(new GameModel());
            this.DataContext = vm;
            this.KeyDown += myBoard.UserControl_KeyDown;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

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

            //PlayerConnected?.Invoke(this, new PlayerConnectedEventArgs(true));
        }

        /*public void Join(string name)
        {

        }*/

    }
}
