using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class BFS<T> : Searcher<T>
    {

        private HashSet<State<T>> closed = new HashSet<State<T>>();
        // get how many nodes were evaluated by the algorithm
        public override int getNumberOfNodesEvaluated()
        {
            return closed.Count;
        }
        public override void addToOpenList(State<T> state)
        {
            openList.Enqueue(state, (float)state.GetCost());
        }
        public override Solution<T> search(ISearchable<T> searchable)
        { // Searcher's abstract method overriding
            addToOpenList(searchable.getInitialState()); // inherited from Searcher
            int OpenListSize = openList.Count;
            while (OpenListSize > 0)
            {
                State<T> n = popOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return backTrace(n); // private method, back traces through the parents
                                         // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContaines(s))
                    {
                        // s.setCameFrom(n);
                        // already done by getSuccess\ ors
                        addToOpenList(s);
                    }
                    else
                    {
                        //...
                    }
                }

                OpenListSize = openList.Count;
            }

        }
        public override Solution<T> backTrace(State<T> goal)
        {
            Solution<T> s = new Solution<T>(goal);
            State<T> thisState = goal;
            while (thisState.GetParent() != null)
            {
                s.trace.Enqueue(thisState);
                thisState = thisState.GetParent();
            }
            return s;

        }
        public override bool openContaines(State<T> state)
        {
            return (openList.Count != 0);
        }
    }
}