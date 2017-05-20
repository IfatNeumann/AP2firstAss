namespace WPFGame
{
    using System.ComponentModel;

    public interface ISinglePlayerModel: INotifyPropertyChanged
    {
        string MazeName { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }

        void StartGame();
    }
}