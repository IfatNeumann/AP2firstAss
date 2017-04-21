using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;


namespace Server
{
    /// <summary>
    /// the class of the Controller
    /// </summary>
    /// <seealso cref="Server.IController" />
    public class Controller : IController
    {
        /// <summary>
        /// a Dictionary that contain the commands
        /// </summary>
        private Dictionary<string, ICommand> commands;
     
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public IModel Model { get; set; }

        /// <summary>
        /// Gets or sets the clientHandler.
        /// </summary>
        /// <value>
        /// The clientHandler.
        /// </value>
        public IClientHandler Ch { get; set; }//
      
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
        }
     
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
  
        /// <summary>
        /// Sets the dictionary.
        /// </summary>
        public void setDic()
        {
            commands.Add("generate", new GenerateMazeCommand(Model));
            commands.Add("solve", new SolveMazeCommand(Model));
            commands.Add("start", new StartMazeCommand(Model));
            commands.Add("list", new ListMazeCommand(Model));
            commands.Add("join", new JoinMazeCommand(Model));
            commands.Add("play", new PlayMazeCommand(Model));
            commands.Add("close", new CloseMazeCommand(Model));
        }
    }
}