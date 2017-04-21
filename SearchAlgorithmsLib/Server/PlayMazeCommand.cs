using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// the 'play' command.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class PlayMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The TcpClient.</param>
        /// <returns>empty string</returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                return "num of arguments not valid";
            }

            string move = args[0];
            if (!(move.Equals("up") || move.Equals("down") || move.Equals("left") || 
                                        move.Equals("right")))
            {
                return "command not valid";
            }

            this.model.PlayMaze(move, client);
            //don't need to return any string for this command
            return string.Empty;
        }
    }
}