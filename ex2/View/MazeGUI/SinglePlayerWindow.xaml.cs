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
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SingleGameViewModel vm;

        public SinglePlayerWindow()
        {
            InitializeComponent();
            //this.DataContext = vm;
            vm = new SingleGameViewModel(new SingleGameModel());
            this.DataContext = vm;
        }

        //public void CreateModel(string name, int rows, int cols)
        public void Update(string name, int rows, int cols)
        {
            vm.MazeName = name;
            vm.Rows = rows;
            vm.Cols = cols;
            vm.StartGame();

            //mazeBoard.DrawMaze();
            mazeBoard.DrawMaze(vm.Rows, vm.Cols, vm.Maze);
            //model = new SingleGameModel(name, rows, cols);
            //vm = new SingleGameViewModel(new SingleGameModel(name, rows, cols));

        }
    }
}
