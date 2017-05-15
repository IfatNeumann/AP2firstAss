namespace WPFGame
{
    public class ISinglePlayerWindowModel
    {
        string Name { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string Maze { get; set; }
        //void SaveSettings();
    }
}