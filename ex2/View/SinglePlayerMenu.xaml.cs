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

namespace ex2
{
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerMenu"/> class.
        /// </summary>
        public SinglePlayerMenu()
        {
            InitializeComponent();
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
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> 
        /// instance containing the event data.</param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            View.MazeGUI.SinglePlayerWindow window = new View.MazeGUI.SinglePlayerWindow();
            window.Update(startMenu.txtMazeName.Text, int.Parse(startMenu.txtRows.Text), 
                int.Parse(startMenu.txtCols.Text));
            window.Show();
            this.Hide();
        }
    }
}
