using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    public class StartMazeCommand :ICommand
    {
        private IModel model;
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }
        
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            if (args.Length != 3 || rows <= 0 || cols <= 0)
                Console.WriteLine("num of arguments not valid");
            if (model.Games.Keys.Contains(name))
                Console.WriteLine("Name already exists");
            model.StartMaze(name, rows, cols,client);
            return "";
        }
    }
}
