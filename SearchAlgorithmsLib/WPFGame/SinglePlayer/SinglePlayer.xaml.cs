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
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        private SinglePlayerViewModel vm;
        private ISinglePlayerModel model;

        public SinglePlayer()
        {
            this.model = new ApplicationSinglePlayerModel();
            this.InitializeComponent();
            this.vm = new SinglePlayerViewModel(this.model);
            this.DataContext = this.vm;
        }

        //private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.vm.MazeRows = this.MazeBoard.Rows;
        //    this.vm.MazeCols = this.MazeBoard.Cols;
        //    this.vm.MazeName = this.MazeBoard.Name;
        //}

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.vm.SaveSettings();
            SinglePlayerGame win = new SinglePlayerGame();
            win.Show();
            this.Close();
        }
    }
}
