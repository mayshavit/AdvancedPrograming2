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
        private SingleGameModel model;

        public SinglePlayerWindow()
        {
            InitializeComponent();
        }

        public void CreateModel(string name, int rows, int cols)
        {
            model = new SingleGameModel(name, rows, cols);
        }
    }
}
