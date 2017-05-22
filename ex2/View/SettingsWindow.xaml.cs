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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;

        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new ApplicationSettingsModel());
            this.DataContext = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            window.Close();
        }

        private void SwitchToMainWindow()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            //this.Close();
            this.Hide();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            SwitchToMainWindow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SwitchToMainWindow();
        }
    }
}
