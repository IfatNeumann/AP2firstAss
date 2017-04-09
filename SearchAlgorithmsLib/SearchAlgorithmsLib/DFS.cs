using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchAlgorithmsLib
{
    public class DFS<T> : Searcher<T>
    {
        public Stack<State<T>> visitedStack = new Stack<State<T>>();

        public override State<T> popDataStructor()
        {
            evaluatedNodes++;
            return visitedStack.Pop();
        }
        public override void addToDataStructor(State<T> state)
        {
            visitedStack.Push(state);
        }

        public override Solution<T> search(ISearchable<T> searchable)
        {
            bool inClosed = false, inVisitedStack = false;
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
                    foreach (State<T> s1 in closed)
                    {
                        if (s.myState.Equals(s1.myState))
                        {
                            inClosed = true;
                            break;
                        }
                    }
                    foreach (State<T> s1 in visitedStack)
                    {
                        if (s.myState.Equals(s1.myState))
                        {
                            inVisitedStack = true;
                            break;
                        }
                    }
                    if (!inClosed && !inVisitedStack)
                    {
                        s.Parent = thisState;// already done by getSuccessors
                        addToDataStructor(s);
                    }
                    inVisitedStack = false;
                    inClosed = false;
                }
            }
            return null;
        }
    }
}
