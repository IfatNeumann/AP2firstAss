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
            InitializeComponent();
            this.vm = new SinglePlayerViewModel(this.model);
            this.DataContext = this.vm;

        }

        //get params from vm
        

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerWindow win = new SinglePlayerWindow(this.model);
            win.Show();
            this.Close();
        }

        //public void Initialize()
        //{
        //    this.md.CreatePoints();

        //    md.Draw();
        //    this.md.M = new MazeLogic(this.md.MazeMatrix, md.SolMatrix , this.md.Start, this.md.End);

        //}
    }
}
