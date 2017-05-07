namespace WPFGame
{
    public interface ISinglePlayerModel
    {
        string MazeName { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
    }
}