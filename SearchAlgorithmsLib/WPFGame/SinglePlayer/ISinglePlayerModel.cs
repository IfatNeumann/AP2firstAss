namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    using MazeLib;

    public interface ISinglePlayerModel: INotifyPropertyChanged
    {
        string MazeName { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        string StringMaze { get; set; }

        Point CurrPoint { get; set; }

        void KeyPressed(char direction);
        void StartGame();
    }
}