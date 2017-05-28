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
using Newtonsoft.Json;

namespace ex2
{
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        private List<GameName> games;
        private string game;

        public MultiPlayerMenu()
        {
            InitializeComponent();
            games = new List<GameName>();
            ListOfGames();

            listOfMazes.ItemsSource = games;
        }

        /*public List<GameName> GameNames
        {
            get { return games; }
            set { games = value; }
        }*/

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            window.Close();
        }

        public void ListOfGames()
        {
            GameModel model = new GameModel();
            string json = model.ListOfMazes();

            List<string> names = JsonConvert.DeserializeObject<List<string>>(json);

 

            for (int i = 0; i < games.Count; i++)
            {
                games.Add(new GameName(names[i]));
            }

            //GameNames = names;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("waiting for another player...", "Start Game");
            View.MazeGUI.MultiPlayerWindow window = new View.MazeGUI.MultiPlayerWindow();
            window.Update(startMenu.txtMazeName.Text, int.Parse(startMenu.txtRows.Text), int.Parse(startMenu.txtCols.Text), true);
            window.Show();
            this.Hide();
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            View.MazeGUI.MultiPlayerWindow window = new View.MazeGUI.MultiPlayerWindow();
            window.Update(game, 0, 0, false);
            window.Show();
            this.Hide();
        }

        private void listOfMazes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.game = listOfMazes.Text;
        }
    }
}
