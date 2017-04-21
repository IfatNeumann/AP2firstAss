using System.Linq;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// class of the close comand 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class CloseMazeCommand:ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
                return "num of arguments not valid";
            string name = args[0];
            if (!model.GamesPlaying.Keys.Contains(name))
                return "Name dosen't exists";
            model.CloseMaze(name, client);
            return "";
        }
    }
}
