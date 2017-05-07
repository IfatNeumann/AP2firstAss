namespace WPFGame
{
    public class ApplicationSinglePlayerModel : ISinglePlayerModel
    {
        public string MazeName
        {
            get
            {
                return Properties.Settings.Default.MazeName;
            }
            set
            {
                Properties.Settings.Default.MazeName = value;
            }
        }

        public int MazeRows
        {
            get
            {
                return Properties.Settings.Default.MazeRows;
            }
            set
            {
                Properties.Settings.Default.MazeRows = value;
            }
        }

        public int MazeCols
        {
            get
            {
                return Properties.Settings.Default.MazeCols;
            }
            set
            {
                Properties.Settings.Default.MazeCols = value;
            }
        }
    }
}