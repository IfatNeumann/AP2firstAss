namespace WPFGame
{
    public class ISinglePlayerGameModel
    {
        string Name { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string Maze { get; set; }
        //void SaveSettings();
    }
}