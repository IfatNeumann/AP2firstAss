using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// class of the join command 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class JoinMazeCommand : ICommand
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
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {

            if (args.Length != 1)
                return "num of arguments not valid";
            string name = args[0];
            if (!model.Games.Keys.Contains(name))
                return "Name dosen't exists";
            //in case the client who started tries to join
            if (model.Games[name].FirstPlayer.Equals(client))
                return "same player!";
            Maze maze = model.JoinMaze(name,client);
            return maze.ToJSON();
        }
    }
}
