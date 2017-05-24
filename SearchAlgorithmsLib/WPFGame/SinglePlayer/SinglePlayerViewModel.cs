namespace WPFGame
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WPFGame.ViewModel" />
    public class SinglePlayerViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISinglePlayerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets or sets the name of the vm.
        /// </summary>
        /// <value>
        /// The name of the vm.
        /// </value>
        public string VmName
        {
            get
            {
                return this.model.MazeName;
            }

            set
            {
                this.model.MazeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm rows.
        /// </summary>
        /// <value>
        /// The vm rows.
        /// </value>
        public int VmRows
        {
            get
            {
                return this.model.MazeRows;
            }

            set
            {
                this.model.MazeRows = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm cols.
        /// </summary>
        /// <value>
        /// The vm cols.
        /// </value>
        public int VmCols
        {
            get
            {
                return this.model.MazeCols;
            }

            set
            {
                this.model.MazeCols = value;
            }
        }
        
    }
}