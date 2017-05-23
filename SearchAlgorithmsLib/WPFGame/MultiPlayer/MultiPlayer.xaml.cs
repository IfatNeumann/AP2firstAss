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

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {

        private MultiPlayerViewModel vm;
        private IMultiPlayerModel model;

        public MultiPlayer()
        {
            this.model = new ApplicationMultiPlayerModel();
            this.InitializeComponent();
            this.vm = new MultiPlayerViewModel(this.model);
            this.DataContext = this.vm;
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            WaitWindow win = new WaitWindow();

            this.model.StartGame();
            MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
            mulWin.Show();
            this.Close();
        }

        private void Join_Button_Click(object sender, RoutedEventArgs e)
        {
            this.model.StartGame();
            MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
            mulWin.Show();
            this.Close();
        }
    }
}
