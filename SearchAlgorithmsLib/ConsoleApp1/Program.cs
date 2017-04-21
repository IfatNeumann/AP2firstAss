using System;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace CompareMazeSolvers
{
    /// <summary>
    /// the class that holds the main function
    /// </summary>
    public class Program
    {
        /// <summary>
        /// the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
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
            // create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze maze = myMazeGen.Generate(row, col);
            Console.WriteLine(maze);
            ObjectAdapter mazeAdapter = new ObjectAdapter(maze);

            // BFS solution
            ISearcher<Position> sbfs = new BFS<Position>();
            sbfs.Search(mazeAdapter);

            // print num of stages
            Console.WriteLine("BFS: " + sbfs.GetNumberOfNodesEvaluated());

            // DFS solution
            ISearcher<Position> sdfs = new DFS<Position>();
            sdfs.Search(mazeAdapter);

            // print num of stages
            Console.WriteLine("DFS: " + sdfs.GetNumberOfNodesEvaluated());
        }
    }
}