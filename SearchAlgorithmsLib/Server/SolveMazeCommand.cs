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
            if (args.Length != 2)
                return "num of arguments not valid";
            if (args[1].Equals("0"))
                algorithm = new BFS<Position>();
            else if (args[1].Equals("1"))
                algorithm = new DFS<Position>();
            else
                return "arguments not valid";
            if (!model.Mazes.Keys.Contains(name))
                return "Name does not exist";

            JObject mazeObj = new JObject();
            mazeObj["Name"] = name;
            mazeObj["Solution"] = model.SolveMaze(name, algorithm);
            mazeObj["NodesEvaluated"] = model.Solutions[model.Mazes[name]].EvaluatedNodes;
            return mazeObj.ToString();
        }
    }
}
