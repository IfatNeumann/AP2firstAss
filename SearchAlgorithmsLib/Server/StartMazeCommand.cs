using System.Linq;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// the 'Start' command.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class StartMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The TcpClient.</param>
        /// <returns> empty string</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            //tests to check if the input is valid
            if (args.Length != 3 || rows <= 0 || cols <= 0)
            {
                return "num of arguments not valid";
            }

            if (this.model.Games.Keys.Contains(name))
            {
                return "Name already exists";
            }

            this.model.StartMaze(name, rows, cols, client);
            //don't need to return any string for this command
            return string.Empty;
        }
    }
}