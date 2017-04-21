using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    public class CloseMazeCommand:ICommand
    {
        private IModel model;
        public CloseMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
                return "num of arguments not valid";
            string name = args[0];
            if (!model.GamesPlaying.Keys.Contains(name))
                return "Name dosen't exists";
            model.CloseMaze(name, client);
            return "";
        }
    }
}
