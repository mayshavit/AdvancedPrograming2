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

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerMenu"/> class.
        /// </summary>
        public MultiPlayerMenu()
        {
            InitializeComponent();
            games = new List<GameName>();
            ListOfGames();

            listOfMazes.ItemsSource = games;
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
        /// Lists the of games.
        /// </summary>
        public void ListOfGames()
        {
            GameModel model = new GameModel();
            string json = model.ListOfMazes();

            List<string> names = JsonConvert.DeserializeObject<List<string>>(json);

            games.Clear();

            for (int i = 0; i < names.Count; i++)
            {
                games.Add(new GameName(names[i]));
            }
        }

        /// <summary>
        /// Handles the Click event of the btnStartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("waiting for another player...", 
                "Start Game", System.Windows.Forms.MessageBoxButtons.OK);
            View.MazeGUI.MultiPlayerWindow window = new View.MazeGUI.MultiPlayerWindow();
            window.Update(startMenu.txtMazeName.Text, int.Parse(startMenu.txtRows.Text), 
                int.Parse(startMenu.txtCols.Text), true);
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnJoin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            View.MazeGUI.MultiPlayerWindow window = new View.MazeGUI.MultiPlayerWindow();
            window.Update(listOfMazes.Text, 0, 0, false);
            window.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the listOfMazes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void listOfMazes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.game = listOfMazes.Text;
        }
    }
}
