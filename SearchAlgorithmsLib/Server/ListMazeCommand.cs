using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using System.Configuration;
using SearchAlgorithmsLib;

namespace Server
{
    public class ListMazeCommand : ICommand
    {
        private IModel model;
        public ListMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            return model.ListMaze();
        }


        }
}
