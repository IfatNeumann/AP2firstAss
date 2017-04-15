using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using ConsoleApp1;

namespace Server
{
    public class Model : IModel
    {
        private IController con;
        public Model(IController con)
        {
            this.con = con;
        }
        private Dictionary<string, Maze> mazes = new Dictionary<string, Maze>();
        public Dictionary<string, Maze> Mazes {get;set;}
        private Dictionary<Maze,Solution<Position>> solutions = new Dictionary<Maze , Solution<Position>>();
        public Model()
        {

        }
        public Maze GenerateMaze( string name, int rows, int cols)
        {
            //create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            mazes.Add(name,myMazeGen.Generate(rows, cols));
            return mazes[name];
        }
        public string SolveMaze (string name, ISearcher<Position> algorithm)
        {
            ObjectAdapter mazeAdapter = new ObjectAdapter(mazes[name]);
            solutions.Add(mazes[name], algorithm.search(mazeAdapter));
            Solution<Position> sol = solutions[mazes[name]];
            Queue<int> way = new Queue<int>();
            State<Position> first, second;
            for (int i=0;i<sol.Trace.Count()-1;++i)
            {
                first = sol.Trace.ElementAt(i);
                second = sol.Trace.ElementAt(i+1);
                //left
                if (first.myState.Col < second.myState.Col)
                    way.Enqueue(0);
                //right
                else if (first.myState.Col > second.myState.Col)
                    way.Enqueue(1);
                //up
                else if (first.myState.Row < second.myState.Row)
                    way.Enqueue(2);
                //down
                else if (first.myState.Row > second.myState.Row)
                    way.Enqueue(3);
            }
            return way.ToString();
        }
        //hi
        public List<string> NamesList()
        {
            return mazes.Keys.ToList();
        }
    }
}
