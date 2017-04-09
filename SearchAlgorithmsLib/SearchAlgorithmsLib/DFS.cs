using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchAlgorithmsLib
{
    class DFS<T> : Searcher<T>
    {
        public Stack<State<T>> visitedStack = new Stack<State<T>>();

        public override State<T> popDataStructor()
        {
            return visitedStack.Pop();
        }
        public override void addToDataStructor(State<T> state)
        {
            visitedStack.Push(state);
        }

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
        }
    }
}
