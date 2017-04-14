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
            List<string> mazes = model.NamesList();
            return mazes.ToString();
        }
    }
}
