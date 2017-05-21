namespace WPFGame
{
    using System.ComponentModel;

    using MazeLib;

    public interface ISinglePlayerModel: INotifyPropertyChanged
    {
        string MazeName { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        string StringMaze { get; set; }

        void StartGame();
    }
}