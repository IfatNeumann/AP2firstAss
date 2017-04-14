using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;
using ConsoleApp1;
using Newtonsoft.Json.Linq;

namespace Server
{
    public class SolveMazeCommand: ICommand
    {
        private IModel model;
        ISearcher<Position> algorithm;
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            if (args[1].Equals("0"))
                algorithm = new BFS<Position>();
            if (args[1].Equals("1"))
                algorithm = new DFS<Position>();

            JObject mazeObj = new JObject();            mazeObj["Name"] = name;            mazeObj["Solution"] = model.SolveMaze(name, algorithm);            mazeObj["NodesEvaluated"] = algorithm.getNumberOfNodesEvaluated();
            return mazeObj.ToString();
        }
    }
}
