using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// class of the generate command 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class GenerateMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>JSON format of the maze</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            if (args.Length != 3 || rows <= 0 || cols <= 0)
            {
                return "num of arguments not valid";
            }

            if (this.model.Mazes.Keys.Contains(name))
            {
                return "Name already exists";
            }
            Maze maze = this.model.GenerateMaze(name, rows, cols);
            return maze.ToJSON();
        }
    }
}