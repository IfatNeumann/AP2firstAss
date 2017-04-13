using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server
{
    public class Controller : AbstractController
    {
        private Dictionary<string, ICommand> commands;
        
        public Controller()
        {
            this.model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(this.model));
            commands.Add("solve", new SolveMazeCommand(this.model));
            commands.Add("start", new StartMazeCommand(this.model));
            commands.Add("list", new ListMazeCommand(this.model));
            commands.Add("play", new PlayMazeCommand(this.model));
            commands.Add("close", new CloseMazeCommand(this.model));
        }
        public override string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
        /*public string HandleRequest(string option, Socket client)
        {
            Dictionary<string, ICommand> commandDic = new Dictionary<string, ICommand>()
            {
                {"Generate", new Generate()},
                {"Solve", new Solve()},
                {"Start", new Start()},
                {"List", new List()},
                {"Play", new Play()},
                {"Close", new Close()}
            };

            char[] whitespace = new char[] { ' ', '\t' };
            string[] split = option.Split(whitespace);
            option = split[0];

            ICommand value;
            string output = "";
            string param1 = "", param2 = "";
            if (split.Length == 2)
            {
                param1 = split[1];
            }
            else if (split.Length == 3)
            {
                param1 = split[1];
                param2 = split[2];
            }

            Params c = new Params(param1, param2, client);

            if (commandDic.ContainsKey(option))
            {
                output = commandDic[option].doMission(c);
            }

            return output;
        }*/
    }
}
