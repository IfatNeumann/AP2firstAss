namespace WPFGame
{
    public class SinglePlayerViewModel : ViewModel
    {
        private ISinglePlayerModel model;

        public SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
        }

        public string MazeName
        {
            get
            {
                return this.model.MazeName;
            }

            set
            {
                this.model.MazeName = value;
                this.NotifyPropertyChanged("MazeName");
            }
        }

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

        public void SaveSettings()
        {
            //this.model.SaveSettings();
        }
    }
}