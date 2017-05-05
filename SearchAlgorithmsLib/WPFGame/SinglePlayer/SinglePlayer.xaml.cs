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
        private SettingsViewModel vm;
        private ISettingsModel model;
        public SinglePlayer()
        {
            this.model = new ApplicationSettingsModel();
            InitializeComponent();
            this.vm = new SettingsViewModel(this.model);
            this.DataContext = this.vm;
        }

            private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.vm.SaveSettings();
            SinglePlayerGame win = new SinglePlayerGame();
            win.Show();
            this.Close();
        }
    }
}
