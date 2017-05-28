﻿using System;
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
        public SinglePlayerMenu()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            window.Close();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //string s = txtMazeName.Text;
            View.MazeGUI.SinglePlayerWindow window = new View.MazeGUI.SinglePlayerWindow();
            //window.CreateModel(txtMazeName.Text, int.Parse(txtRows.Text), int.Parse(txtCols.Text));
            window.Update(startMenu.txtMazeName.Text, int.Parse(startMenu.txtRows.Text), int.Parse(startMenu.txtCols.Text));
            window.Show();
            this.Hide();
        }
    }
}
