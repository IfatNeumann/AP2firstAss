using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract class searcher 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
        /// Adds to data structor.
        /// </summary>
        /// <param name="state">The state of the player.</param>
        public abstract void addToDataStructor(State<T> state);

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher"/> class.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }
       
        /// <summary>
        /// Pops from data structor.
        /// </summary>
        /// <returns></returns>
        public abstract State<T> popDataStructor();
    

        /// <summary>
        /// the search method
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable object.</param>
        /// <returns></returns>
        public abstract Solution<T> search(ISearchable<T> searchable);
  

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns></returns>
        public int getNumberOfNodesEvaluated()
        {
            return closed.Count;
        }
    }
}
