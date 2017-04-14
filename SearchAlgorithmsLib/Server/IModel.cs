using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;

namespace Server
{
    public interface IModel
    {
        Dictionary<string, Maze> Mazes { get; set; }
        Maze GenerateMaze( string name, int rows, int cols);
        string SolveMaze(string name, ISearcher<Position> algorithm);
        List<string> NamesList();
    }
    
}
