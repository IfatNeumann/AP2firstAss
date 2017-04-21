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
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            this.commands = new Dictionary<string, ICommand>();
        }

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
        public IClientHandler Ch { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>the output of the command</returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!this.commands.ContainsKey(commandKey))
            {
                return "Command not found";
            }

            string[] args = arr.Skip(1).ToArray();
            ICommand command = this.commands[commandKey];
            return command.Execute(args, client);
        }

        /// <summary>
        /// Sets the dictionary.
        /// </summary>
        public void SetDic()
        {
            this.commands.Add("generate", new GenerateMazeCommand(this.Model));
            this.commands.Add("solve", new SolveMazeCommand(this.Model));
            this.commands.Add("start", new StartMazeCommand(this.Model));
            this.commands.Add("list", new ListMazeCommand(this.Model));
            this.commands.Add("join", new JoinMazeCommand(this.Model));
            this.commands.Add("play", new PlayMazeCommand(this.Model));
            this.commands.Add("close", new CloseMazeCommand(this.Model));
        }
    }
}