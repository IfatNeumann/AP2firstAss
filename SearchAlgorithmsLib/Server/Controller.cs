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
        private IView view;
        private IModel model;
        private Dictionary<string, ICommand> commands;

        public IView View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
            }
        }
        public IModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }


        public Controller()
        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("Generate", new Generate(model));
            commands.Add("Solve", new Solve(model));
            commands.Add("Start", new Start(model));
            commands.Add("List", new List(model));
            commands.Add("Play", new Play(model));
            commands.Add("Close", new Close(model));
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
}
