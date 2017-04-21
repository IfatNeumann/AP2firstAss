using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace CompareMazeSolvers
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            CompareSolvers(100, 100);
            Console.Read();
        }
        /// <summary>
        /// Compares the solvers.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        public static void CompareSolvers(int row, int col)
        {
            //create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze maze = myMazeGen.Generate(row, col);
            Console.WriteLine(maze);
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
