using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server
{
    public class Controller : IController
    {
        private IModel model = new Model();
        private IClientHandler ch = new ClientHandler();
        private Dictionary<string, ICommand> commands;
        public IModel Model { get; set; }
        public IClientHandler Ch { get; set; }//
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(this.model));
            commands.Add("solve", new SolveMazeCommand(this.model));
            commands.Add("start", new StartMazeCommand(this.model));
            commands.Add("list", new ListMazeCommand(this.model));
            commands.Add("play", new PlayMazeCommand(this.model));
            commands.Add("close", new CloseMazeCommand(this.model));
        }
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
    }
}///////