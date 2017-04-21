using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// class of the list command 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class ListMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the list</returns>
        public string Execute(string[] args, TcpClient client)
        {
            return this.model.ListMaze();
        }
    }
}