using System.Media;
using System.Windows;

namespace WPFGame
{
    using WPFGame.OtherWindows;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SettingsViewModel vm;

        /// <summary>
        /// The model
        /// </summary>
        private ISettingsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            //SoundPlayer player = new SoundPlayer(@"C:\Users\m1245\Source\Repos\AP2firstAss\SearchAlgorithmsLib\WPFGame\music\openMusic.wav");
            //player.Load();
            //player.Play();
            this.model = new ApplicationSettingsModel();
            this.InitializeComponent();
            this.vm = new SettingsViewModel(this.model);
            this.DataContext = this.vm;
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.TxtIp.Text.Equals(string.Empty)
                || this.TxtPort.Text.Equals(string.Empty)
                || this.TxtCols.Text.Equals(string.Empty)
                || this.TxtRows.Text.Equals(string.Empty))
            {
                CheckArgsWindow win = new CheckArgsWindow();
                win.Show();
            }
            else
            {
                this.vm.SaveSettings();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
