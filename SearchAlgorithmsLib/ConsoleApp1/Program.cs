using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;

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
            int rows = 5, cols = 5;
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze myMaze = myMazeGen.Generate(rows,cols);
            ObjectAdapter mazeAdapter = new ObjectAdapter(myMaze);
            //print maze
            Console.WriteLine(myMaze.ToString());
            //BFS solution
            //DFS solution
            //print num of stages
        }
    }
}
