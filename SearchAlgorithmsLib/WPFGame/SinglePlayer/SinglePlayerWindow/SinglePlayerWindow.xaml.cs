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
            this.vm.SaveSettings();
            SinglePlayerWindow win = new SinglePlayerWindow(this.model);
            win.Show();
            this.Close();
        }

        private void Solve_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            this.MazeBoard.SolveMaze(this.vm.VmSolution);
            //this.vm.SaveSettings();
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

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    {
                        this.vm.KeyPressed('l');
                        break;
                    }
                case Key.Right:
                    {
                        this.vm.KeyPressed('r');
                        break;
                    }
                case Key.Up:
                    {
                        this.vm.KeyPressed('u');
                        break;
                    }
                case Key.Down:
                    {
                        this.vm.KeyPressed('d');
                        break;
                    }
            }
        }
    }
}