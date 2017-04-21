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
        /// Closed contain the nodes we visited
        /// </summary>
        private HashSet<State<T>> closed;

        /// <summary>
        /// The evaluated nodes
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        protected Searcher()
        {
            this.EvaluatedNodes = 0;
            this.Closed = new HashSet<State<T>>();
        }

        /// <summary>
        /// Gets or sets 'closed' hash-set
        /// </summary>
        /// <value>
        /// closed - the hash-set.
        /// </value>
        protected HashSet<State<T>> Closed
        {
            get
            {
                return this.closed;
            }

            set
            {
                this.closed = value;
            }
        }

        /// <summary>
        /// Gets or sets 'evaluatedNodes'
        /// </summary>
        /// <value>
        /// 'evaluatedNodes' - int.
        /// </value>
        protected int EvaluatedNodes
        {
            get
            {
                return this.evaluatedNodes;
            }

            set
            {
                this.evaluatedNodes = value;
            }
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
            return this.Closed.Count;
        }
    }
}