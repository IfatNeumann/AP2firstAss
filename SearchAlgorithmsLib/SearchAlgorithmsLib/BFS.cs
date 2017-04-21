using System.Collections.Generic;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the class of the bfs algorithm
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class BFS<T> : Searcher<T>
    {
        /// <summary>
        /// priority queue of the open list
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}"/> class.
        /// </summary>
        public BFS()
        {
            this.openList = new SimplePriorityQueue<State<T>>();
        }

        /// <summary>
        /// Adds to data structure.
        /// </summary>
        /// <param name="state">The state.</param>
        public override void AddToDataStructure(State<T> state)
        {
            this.openList.Enqueue(state, (float)state.Cost);
        }

        /// <summary>
        /// Pops the data structure.
        /// </summary>
        /// <returns>the state at the top of the data structure</returns>
        public override State<T> PopDataStructure()
        {
            this.EvaluatedNodes++;
            return this.openList.Dequeue();
        }

        /// <summary>
        /// Searches the specified search able.
        /// according to bfs algorithm
        /// </summary>
        /// <param name="searchable">The search able.</param>
        /// <returns>the solution</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            // Searcher's abstract method overriding
            this.AddToDataStructure(searchable.GetInitialState()); // inherited from Searcher
            int openListSize = this.openList.Count;
            while (openListSize > 0)
            {
                State<T> n = this.PopDataStructure(); // inherited from Searcher, removes the best state
                this.Closed.Add(n);
                if (n.Equals(searchable.GetGoalState()))
                {
                    return n.BackTrace(); // private method, back traces through the parents
                }

                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> successors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in successors)
                {
                    if (!this.Closed.Contains(s) && !this.openList.Contains(s))
                    {
                        s.Parent = n; // already done by getSuccessors
                        s.Cost = n.Cost + 1;
                        this.AddToDataStructure(s);
                    }
                    else if (this.openList.Contains(s) || (n.Cost + 1 < s.Cost))
                    {
                        // is inside the open list
                        this.openList.Remove(s);
                        s.Cost = n.Cost + 1;
                        s.Parent = n;
                        this.AddToDataStructure(s);
                    }
                }

                openListSize = this.openList.Count;
            }

            return null;
        }
    }
}