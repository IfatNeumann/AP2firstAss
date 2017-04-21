using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    public class PlayMazeCommand:ICommand
    {
        private IModel model;
        public PlayMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string move = args[0];
            if (!((move.Equals("up")) || (move.Equals("down")) ||
                 (move.Equals("left")) || (move.Equals("right"))))
                return "command not valid";
            if (args.Length != 1)
                return "num of arguments not valid";
            model.PlayMaze(move, client);
            return "";
        }
    }
}
