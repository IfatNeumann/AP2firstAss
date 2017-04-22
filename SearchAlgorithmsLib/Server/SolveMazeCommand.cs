using System.Linq;
using System.Net.Sockets;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace Server
{
    /// <summary>
    /// the 'solve' command.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class SolveMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// The algorithm
        /// </summary>
        private ISearcher<Position> algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The TcpClient.</param>
        /// <returns>empty string</returns>
        public string Execute(string[] args, TcpClient client)
        {
            // tests to check if the input is valid
            if (args.Length != 2)
            {
                return "num of arguments not valid";
            }

            string name = args[0];

            if (args[1].Equals("0"))
            {
                this.algorithm = new BFS<Position>();
            }
            else if (args[1].Equals("1"))
            {
                this.algorithm = new DFS<Position>();
            }
            else
            {
                return "arguments not valid";
            }

            if (!this.model.Mazes.Keys.Contains(name))
            {
                return "Name does not exist";
            }

            JObject mazeObj = new JObject();
            mazeObj["Name"] = name;
            mazeObj["Solution"] = this.model.SolveMaze(name, this.algorithm);
            mazeObj["NodesEvaluated"] = this.model.Solutions[this.model.Mazes[name]].EvaluatedNodes;
            return mazeObj.ToString();
        }
    }
}