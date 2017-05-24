
namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    using MazeLib;

    /// <summary>
    /// the inteface of the single player model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface ISinglePlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        string MazeName { get; set; }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        int MazeRows { get; set; }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        int MazeCols { get; set; }

        /// <summary>
        /// Gets or sets the string maze.
        /// </summary>
        /// <value>
        /// The string maze.
        /// </value>
        string StringMaze { get; set; }

        /// <summary>
        /// Gets or sets the curr point.
        /// </summary>
        /// <value>
        /// The curr point.
        /// </value>
        Point CurrPoint { get; set; }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        Point EndPoint { get; }

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        string Solution { get; set; }

        /// <summary>
        /// Keys the pressed.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        int KeyPressed(char direction);

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Solves the maze.
        /// </summary>
        void SolveMaze();

        /// <summary>
        /// Initializes the start position.
        /// </summary>
        void InitStartPos();
    }
}