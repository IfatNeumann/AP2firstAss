using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace CompareMazeSolvers
{
    /// <summary>
    /// adupt the maze to the program
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{MazeLib.Position}" />
    public class ObjectAdapter : ISearchable<Position>
    {
        /// <summary>
        /// My maze
        /// </summary>
        private Maze myMaze;
        //Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectAdapter"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public ObjectAdapter(Maze maze)
        {
            this.myMaze = maze;
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            int col = s.myState.Col, row = s.myState.Row;
            int upperBound = 0, lowerBound = myMaze.Rows, rightBound = myMaze.Cols, leftBound = 0;
            List<State<Position>> neighbors = new List<State<Position>>();
            //12 o'clock
            if ((row - 1 >= upperBound) && (myMaze[row - 1, col] == CellType.Free))
                neighbors.Add(State<Position>.getState(new Position(row - 1, col)));
            //3 o'clock
            if ((col + 1 < rightBound) && (myMaze[row, col + 1] == CellType.Free))
                neighbors.Add(State<Position>.getState(new Position(row, col + 1)));
            //6 o'clock
            if ((row + 1 < lowerBound) && (myMaze[row + 1, col] == CellType.Free))
                neighbors.Add(State<Position>.getState(new Position(row + 1, col)));
            //9 o'clock
            if ((col - 1 >= leftBound) && (myMaze[row, col - 1] == CellType.Free))
                neighbors.Add(State<Position>.getState(new Position(row, col - 1)));
            return neighbors;
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<Position> getGoalState()
        {
            return new State<Position>(myMaze.GoalPos);
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> getInitialState()
        {
            return new State<Position>(myMaze.InitialPos);
        }
    }
}
