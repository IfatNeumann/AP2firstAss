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
using MazeLib;

namespace WPFGame
{
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SinglePlayerWindowViewModel vm;

        private ISinglePlayerModel model;


        public SinglePlayerWindow(ISinglePlayerModel model)
        {
        
                this.model = model;
                this.InitializeComponent();
                this.vm = new SinglePlayerWindowViewModel(model);
                this.DataContext = this.vm;
        }

        private void Restart_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            this.vm.InitStartPos();
            SinglePlayerWindow win = new SinglePlayerWindow(this.model);
            win.Show();
            this.Close();
        }

        private void Solve_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            this.model.SolveMaze();
            //MainWindow win = (MainWindow)Application.Current.MainWindow;
            //win.Show();
            //this.Close();
        }

        private void Back_To_Main_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        public void winScreen()
        {
            WinWindow win = new WinWindow();
            win.Show();
            this.Close();
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            int result;
            switch (e.Key)
            {
                
                case Key.Left:
                    {
                        result = this.vm.KeyPressed('l');
                        if (result == 1)
                        {
                            winScreen();
                        }
                        break;
                    }
                case Key.Right:
                    {
                        result = this.vm.KeyPressed('r');
                        if (result == 1)
                        {
                            winScreen();
                        }
                        break;
                    }
                case Key.Up:
                    {
                        result = this.vm.KeyPressed('u');
                        if (result == 1)
                        {
                            winScreen();
                        }
                        break;
                    }
                case Key.Down:
                    {
                        result = this.vm.KeyPressed('d');
                        if (result == 1)
                        {
                            winScreen();
                        }
                        break;
                    }

            }
            
        }
    }
}
