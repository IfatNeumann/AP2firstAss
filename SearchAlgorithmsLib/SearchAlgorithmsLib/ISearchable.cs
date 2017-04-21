using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// an interface of search able object
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>the initial state</returns>
        State<T> GetInitialState();

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>the goal state</returns>
        State<T> GetGoalState();

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The given state</param>
        /// <returns>a list of all the possible states</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}