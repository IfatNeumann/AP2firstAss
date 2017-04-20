using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    class JoinMazeCommand : ICommand
    {
        private IModel model;
        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            if (args.Length != 1)
                Console.WriteLine("num of arguments not valid");

            if (!model.Games.Keys.Contains(name))
                Console.WriteLine("Name dosen't exists");
            Maze maze = model.JoinMaze(name,client);
            return maze.ToJSON();
        }
    }
}
