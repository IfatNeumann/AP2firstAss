namespace SearchAlgorithmsLib
{
    /// <summary>
    /// searcher interface s
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// the Search method
        /// Searches the specified Search able.
        /// </summary>
        /// <param name="searchable">The Search able.</param>
        /// <returns>the solution</returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Adds to data structure.
        /// </summary>
        /// <param name="state">The state.</param>
        void AddToDataStructure(State<T> state);

        /// <summary>
        /// Pops the data structure.
        /// </summary>
        /// <returns>the state at the top of the data structure</returns>
        State<T> PopDataStructure();

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns>the number of evaluated nodes</returns>
        int GetNumberOfNodesEvaluated();
    }
}