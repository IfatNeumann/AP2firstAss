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
        Dictionary<string, Maze> Mazes { get; }
        Dictionary<string, Game> Games { get; }
        Dictionary<string, Game> GamesPlaying { get; }
        Dictionary<Maze, Solution<Position>> Solutions { get; }
        Maze GenerateMaze( string name, int rows, int cols);
        string SolveMaze(string name, ISearcher<Position> algorithm);
        void StartMaze(string name, int rows, int cols, TcpClient client);
        string ListMaze();
        Maze JoinMaze(string name, TcpClient client);
        void PlayMaze(string move, TcpClient client);
        void CloseMaze(string name ,TcpClient client);
    }
    
}
