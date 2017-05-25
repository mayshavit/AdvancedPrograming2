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

namespace ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsModel_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow win = new SettingsWindow();
            win.Show();
            this.Hide();
        }

        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerMenu menu = new SinglePlayerMenu();
            menu.ShowDialog();
            this.Hide();
        }
    }
}
