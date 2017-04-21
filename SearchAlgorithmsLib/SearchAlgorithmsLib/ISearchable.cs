using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// an interface of sercable object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>{
        
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> getInitialState();
        
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<T> getGoalState();
        
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
