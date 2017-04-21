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
        /// The evaluated nodes
        /// </summary>
        private Stack<State<T>> visitedStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        public DFS()
        {
            this.visitedStack = new Stack<State<T>>();
        }

        /// <summary>
        /// Gets or sets the visitedStack.
        /// </summary>
        /// <value>
        /// The visited stack.
        /// </value>
        public Stack<State<T>> VisitedStack
        {
            get
            {
                return this.visitedStack;
            }

            set
            {
                value = this.visitedStack;
            }
        }

        /// <summary>
        /// Pops the data structure.
        /// </summary>
        /// <returns>the state at the top of the data structure</returns>
        public override State<T> PopDataStructure()
        {
            this.EvaluatedNodes++;
            return this.VisitedStack.Pop();
        }

        /// <summary>
        /// Adds to data structure.
        /// </summary>
        /// <param name="state">The state.</param>
        public override void AddToDataStructure(State<T> state)
        {
            this.VisitedStack.Push(state);
        }

        /// <summary>
        /// Searches the specified search able.
        /// according to dfs algorithm
        /// </summary>
        /// <param name="searchable">The search able.</param>
        /// <returns>the solution</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            this.VisitedStack.Push(searchable.GetInitialState());
            while (this.VisitedStack.Count != 0)
            {
                State<T> thisState = this.VisitedStack.Pop();
                this.Closed.Add(thisState);
                if (thisState.Equals(searchable.GetGoalState()))
                {
                    return thisState.BackTrace();
                }

                List<State<T>> successors = searchable.GetAllPossibleStates(thisState);
                foreach (State<T> s in successors)
                {
                    if (!this.Closed.Contains(s) && !this.VisitedStack.Contains(s))
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