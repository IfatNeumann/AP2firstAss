using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareSolvers(15, 15);
            Console.Read();
        }
        public static void CompareSolvers(int row, int col)
        {
            //create maze
            Maze maze;
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            maze = myMazeGen.Generate(row, col);
            Console.WriteLine(maze);//print
            Console.WriteLine("Starting point: "+maze.InitialPos.ToString());
            Console.WriteLine("Goal point: " + maze.GoalPos.ToString());
            // Maze myMaze = myMazeGen.Generate(rows,cols);
            ObjectAdapter mazeAdapter = new ObjectAdapter(maze);
            //BFS solution
            ISearcher<Position> sbfs = new BFS<Position>();
            sbfs.search(mazeAdapter);
            //print num of stages
            Console.WriteLine("BFS: " + sbfs.getNumberOfNodesEvaluated());
            //DFS solution
            ISearcher<Position> sdfs = new DFS<Position>();
            sdfs.search(mazeAdapter);
            //print num of stages
            Console.WriteLine("DFS: "+sdfs.getNumberOfNodesEvaluated()); 
        }
    }
}
