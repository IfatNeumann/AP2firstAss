using System.Windows;


namespace WPFGame
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayer : Window
    {
        /// <summary>
        /// The vm of the single player
        /// </summary>
        private SinglePlayerViewModel vm;
        /// <summary>
        /// The model
        /// </summary>
        private ISinglePlayerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayer"/> class.
        /// </summary>
        public SinglePlayer()
        {

            this.model = new ApplicationSinglePlayerModel();
            this.InitializeComponent();
            this.vm = new SinglePlayerViewModel(this.model);
            this.DataContext = this.vm;

        }

        /// <summary>
        /// Handles the Click event of the Start_Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            this.model.StartGame();
            SinglePlayerWindow win = new SinglePlayerWindow(this.model);
            win.Show();
            this.Close();
        }
    }
}
