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
            model.StartMaze(name, rows, cols,client);
            //only when there is another client
            while (!model.GamesPlaying.ContainsKey(name))
            {

            }
            return model.GamesPlaying[name].MyMaze.ToJSON();
        }
    }
}
