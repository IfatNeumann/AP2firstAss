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
        ObjectAdapter(Maze maze)
        {
            myMaze = maze;
        }

        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            throw new NotImplementedException();
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
