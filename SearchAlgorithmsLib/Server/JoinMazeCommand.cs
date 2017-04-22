using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// class of the join command 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class JoinMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the maze's JSON</returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                return "num of arguments not valid";
            }

            string name = args[0];
            if (!this.model.Games.Keys.Contains(name))
            {
                return "Name doesn't exists";
            }

            // in case the client who started tries to join
            if (this.model.Games[name].FirstPlayer.Equals(client))
            {
                return "same player!";
            }

            Maze maze = this.model.JoinMaze(name, client);
            return maze.ToJSON();
        }
    }
}