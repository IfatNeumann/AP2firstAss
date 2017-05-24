using System.Windows;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for LoseWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class LoseWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoseWindow"/> class.
        /// </summary>
        public LoseWindow()
        {
            this.InitializeComponent();
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
    }
}
