using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class of the dfs algoritm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// The visited stack
        /// contain all the nodes we already visited
        /// </summary>
        public Stack<State<T>> visitedStack = new Stack<State<T>>();

        /// <summary>
        /// Pops the data structor.
        /// </summary>
        /// <returns></returns>
        public override State<T> popDataStructor()
        {
            evaluatedNodes++;
            return visitedStack.Pop();
        }
        /// <summary>
        /// Adds to data structor.
        /// </summary>
        /// <param name="state">The state.</param>
        public override void addToDataStructor(State<T> state)
        {
            visitedStack.Push(state);
        }

        /// <summary>
        /// Searches the specified searchable.
        /// acording to dfs algoritm
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            visitedStack.Push(searchable.getInitialState());
            while (visitedStack.Count != 0)
            {
                State<T> thisState = visitedStack.Pop();
                closed.Add(thisState);
                if (thisState.Equals(searchable.getGoalState()))
                    return thisState.backTrace();
                List<State<T>> succerssors = searchable.getAllPossibleStates(thisState);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !visitedStack.Contains(s))
                    {
                        s.Parent = thisState;// already done by getSuccessors
                        addToDataStructor(s);
                    }
                }
            }
            return null;
        }
    }
}
