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

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MainWindowVM vm;

        public MainWindow()
        {
            this.InitializeComponent();
           // this.vm = new MainWindowVM();
            //this.DataContext = this.vm;
        }

        private void Single_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayer singleWin = new SinglePlayer();
            singleWin.Show();
            this.Close();
        }

        private void Multi_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayer multiWin = new MultiPlayer();
            multiWin.Show();
            this.Close();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWin = new SettingsWindow();
            settingsWin.Show();
            this.Hide();
        }

    }
}
