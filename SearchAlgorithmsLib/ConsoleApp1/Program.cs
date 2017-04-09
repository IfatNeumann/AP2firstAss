using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        void CompareSolvers()
        {
            //create maze
            DFSMazeGenerator myMaze = new DFSMazeGenerator();
            ObjectAdapter mazeAdapter = new ObjectAdapter(myMaze);
            //print maze
            Console.WriteLine(myMaze.ToString());
            //BFS solution
            //DFS solution
            //print num of stages
        }
    }
}
