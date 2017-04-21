
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// searcher interface s
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T>
    {

        /// <summary>
        /// the search method
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        Solution<T> search(ISearchable<T> searchable);
        
        /// <summary>
        /// Adds to data structor.
        /// </summary>
        /// <param name="state">The state.</param>
        void addToDataStructor(State<T> state);
        
        /// <summary>
        /// Pops the data structor.
        /// </summary>
        /// <returns></returns>
        State<T> popDataStructor();
   

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns></returns>
        int getNumberOfNodesEvaluated();
    }
}
