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
        private Dictionary<string, ICommand> commands;
        public IModel Model { get; set; }
        public IClientHandler Ch { get; set; }//
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
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
        public void setDic()
        {
            commands.Add("generate", new GenerateMazeCommand(Model));
            commands.Add("solve", new SolveMazeCommand(Model));
            commands.Add("start", new StartMazeCommand(Model));
            commands.Add("list", new ListMazeCommand(Model));
            commands.Add("play", new PlayMazeCommand(Model));
            commands.Add("close", new CloseMazeCommand(Model));
        }
    }
}///////