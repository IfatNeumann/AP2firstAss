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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private MultiPlayerWindowViewModel vm;

        /// <summary>
        /// The model
        /// </summary>
        private IMultiPlayerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindow"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerWindow(IMultiPlayerModel model)
        {

            this.model = model;
            this.InitializeComponent();
            this.vm = new MultiPlayerWindowViewModel(model);
            this.DataContext = this.vm;
            this.vm.ClosingHappend += this.CloseGame;
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="reason">The reason.</param>
        private void CloseGame(string reason)
        {
            if (reason.Equals("lose"))
            {
                this.Dispatcher.BeginInvoke(
                    (Action)(() =>
                        {
                            LoseWindow win = new LoseWindow();
                            win.Show();
                            this.Close();
                        }));
            }
            else if (reason.Equals("technicalWin"))
            {
                this.Dispatcher.BeginInvoke(
                    (Action)(() =>
                        {
                            TechnicalWinWindow win = new TechnicalWinWindow();
                            win.Show();
                            this.Close();
                        }));
            }
        }





        /// <summary>
        /// Handles the Click event of the Back_To_Main_Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Back_To_Main_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        /// <summary>
        /// Wins the screen.
        /// </summary>
        public void winScreen()
        {
            WinWindow win = new WinWindow();
            win.Show();
            this.Close();
        }

        /// <summary>
        /// Keys down handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Loaded event of the MazeBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the OnClosed event of the MultiPlayerWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MultiPlayerWindow_OnClosed(object sender, EventArgs e)
        {
            this.vm.CloseGame();
        }
    }
}
