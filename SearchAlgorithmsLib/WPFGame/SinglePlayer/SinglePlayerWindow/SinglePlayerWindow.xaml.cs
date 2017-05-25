using System.Windows;
using System.Windows.Input;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SinglePlayerWindowViewModel vm;

        /// <summary>
        /// The model
        /// </summary>
        private ISinglePlayerModel model;


        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindow"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerWindow(ISinglePlayerModel model)
        {
                this.model = model;
                this.InitializeComponent();
                this.vm = new SinglePlayerWindowViewModel(model);
                this.DataContext = this.vm;
        }

        /// <summary>
        /// Handles the Click event of the Restart_Game_Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Restart_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            this.vm.InitStartPos();
            SinglePlayerWindow win = new SinglePlayerWindow(this.model);
            win.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the Solve_Game_Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Solve_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            this.vm.InitStartPos();
            this.model.SolveMaze();
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
        public void WinScreen()
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
            int result = 0;
            switch (e.Key)
            {
                case Key.Left:
                    {
                        result = this.vm.KeyPressed('l');
                        break;
                    }
                case Key.Right:
                    {
                        result = this.vm.KeyPressed('r');
                        break;
                    }
                case Key.Up:
                    {
                        result = this.vm.KeyPressed('u');
                        break;
                    }
                case Key.Down:
                    {
                        result = this.vm.KeyPressed('d');
                        break;
                    }
            }

            if (result == 1)
            {
                this.WinScreen();
            }
        }
    }
}
