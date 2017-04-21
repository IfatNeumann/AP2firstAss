using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract class searcher 
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// closed contain the nodes we visited
        /// </summary>
        public HashSet<State<T>> closed = new HashSet<State<T>>();

        /// <summary>
        /// The evaluated nodes
        /// </summary>
        public int evaluatedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher"/> class.
        /// </summary>
        public Searcher()
        {
            this.evaluatedNodes = 0;
        }

        /// <summary>
        /// Adds to data structure.
        /// </summary>
        /// <param name="state">The state of the player.</param>
        public abstract void AddToDataStructure(State<T> state);

        /// <summary>
        /// Pops from data structure.
        /// </summary>
        /// <returns>the state at the top of the data structure</returns>
        public abstract State<T> PopDataStructure();

        /// <summary>
        /// the Search method
        /// Searches the specified Search able.
        /// </summary>
        /// <param name="searchable">The search able object.</param>
        /// <returns>the solution that the searcher found</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns>the number of the evaluated nodes</returns>
        public int GetNumberOfNodesEvaluated()
        {
            return this.closed.Count;
        }
    }
}