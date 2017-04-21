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
