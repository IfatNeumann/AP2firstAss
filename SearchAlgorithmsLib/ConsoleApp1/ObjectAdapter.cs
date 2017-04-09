using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace ConsoleApp1
{
    class ObjectAdapter : ISearchable<Position>
    {
        private Maze myMaze;
        //Ctor
        public ObjectAdapter(Maze maze)
        {
            this.myMaze = maze;
        }

        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            List<State<Position>> neighbors = new List<State<Position>>();
        }

        public State<Position> getGoalState()
        {
            return new State<Position>(myMaze.GoalPos);
        }

        public State<Position> getInitialState()
        {
            return new State<Position>(myMaze.InitialPos);
        }
    }
}
