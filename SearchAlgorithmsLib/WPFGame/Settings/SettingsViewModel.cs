namespace WPFGame
{
    /// <summary>
    /// the view model of the settings' window
    /// </summary>
    /// <seealso cref="WPFGame.ViewModel" />
    public class SettingsViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISettingsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIP
        {
            get
            {
                return this.model.ServerIP;
            }

            set
            {
                this.model.ServerIP = value;
                this.NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public int ServerPort
        {
            get
            {
                return this.model.ServerPort;
            }

            set
            {
                this.model.ServerPort = value;
                this.NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public int MazeRows
        {
            get
            {
                return this.model.MazeRows;
            }

            set
            {
                this.model.MazeRows = value;
                this.NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public int MazeCols
        {
            get
            {
                return this.model.MazeCols;
            }

            set
            {
                this.model.MazeCols = value;
                this.NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>
        /// The search algorithm.
        /// </value>
        public int SearchAlgorithm
        {
            get
            {
                return this.model.SearchAlgorithm;
            }

            set
            {
                this.model.SearchAlgorithm = value;
                this.NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.model.SaveSettings();
        }
    }
}
