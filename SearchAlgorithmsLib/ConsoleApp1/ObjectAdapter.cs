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
        private bool positionIsTheParent(State<Position> s, Position p)
        {
            int parentCol = s.Parent.myState.Col, parentRow = s.Parent.myState.Row;
            return ((parentCol == p.Col) && (parentRow == p.Row));
        }
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            int col = s.myState.Col, row = s.myState.Row;
            int upperBound = 0, lowerBound = myMaze.Rows, rightBound = myMaze.Cols, leftBound = 0;
            List<State<Position>> neighbors = new List<State<Position>>();
            //12 o'clock
            if ((row - 1 > upperBound) && (myMaze[row - 1, col] == CellType.Free))
                if (!positionIsTheParent(s, new Position(row - 1, col)))
                    neighbors.Add(new State<Position>(new Position(row - 1, col)));
            //3 o'clock
            if ((col + 1 < rightBound) && (myMaze[row, col + 1] == CellType.Free))
                if (!positionIsTheParent(s, new Position(row, col + 1)))
                    neighbors.Add(new State<Position>(new Position(row, col + 1)));
            //6 o'clock
            if ((row + 1 < lowerBound) && (myMaze[row + 1, col] == CellType.Free))
                if (!positionIsTheParent(s, new Position(row + 1, col)))
                    neighbors.Add(new State<Position>(new Position(row + 1, col)));
            //9 o'clock
            if ((col - 1 > leftBound) && (myMaze[row, col - 1] == CellType.Free))
                if(!positionIsTheParent(s,new Position(row, col - 1)))
                    neighbors.Add(new State<Position>(new Position(row, col - 1)));
            return neighbors;
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
