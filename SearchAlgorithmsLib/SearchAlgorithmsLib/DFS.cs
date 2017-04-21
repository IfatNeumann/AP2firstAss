using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class of the dfs algorithm
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// The visited stack
        /// contain all the nodes we already visited
        /// </summary>
        public Stack<State<T>> visitedStack = new Stack<State<T>>();

        /// <summary>
        /// Pops the data structure.
        /// </summary>
        /// <returns>the state at the top of the data structure</returns>
        public override State<T> PopDataStructure()
        {
            this.evaluatedNodes++;
            return this.visitedStack.Pop();
        }

        /// <summary>
        /// Adds to data structure.
        /// </summary>
        /// <param name="state">The state.</param>
        public override void AddToDataStructure(State<T> state)
        {
            this.visitedStack.Push(state);
        }

        /// <summary>
        /// Searches the specified search able.
        /// according to dfs algorithm
        /// </summary>
        /// <param name="searchable">The search able.</param>
        /// <returns>the solution</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            this.visitedStack.Push(searchable.GetInitialState());
            while (this.visitedStack.Count != 0)
            {
                State<T> thisState = this.visitedStack.Pop();
                this.closed.Add(thisState);
                if (thisState.Equals(searchable.GetGoalState()))
                {
                    return thisState.BackTrace();
                }

                List<State<T>> successors = searchable.GetAllPossibleStates(thisState);
                foreach (State<T> s in successors)
                {
                    if (!this.closed.Contains(s) && !this.visitedStack.Contains(s))
                    {
                        s.Parent = thisState;
                        this.AddToDataStructure(s);
                    }
                }
            }

            return null;
        }
    }
}