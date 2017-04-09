using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : Searcher<T>
    {
        private SimplePriorityQueue<State<T>> openList =new SimplePriorityQueue<State<T>>();
        // get how many nodes were evaluated by the algorithm
        public override void addToDataStructor(State<T> state)
        {
            openList.Enqueue(state, (float)state.Cost);
        }
        public override State<T> popDataStructor()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }
        public override Solution<T> search(ISearchable<T> searchable)
        { // Searcher's abstract method overriding
            addToDataStructor(searchable.getInitialState()); // inherited from Searcher
            int OpenListSize = openList.Count;
            while (OpenListSize > 0)
            {
                State<T> n = popDataStructor(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return n.backTrace(); // private method, back traces through the parents
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openList.Contains(s))
                    {
                        s.Parent = n;// already done by getSuccessors
                        s.Cost = n.Cost + 1;
                        addToDataStructor(s);
                    }
                    else if (openList.Contains(s) || (n.Cost + 1 < s.Cost))//is inside the open list
                    {
                        openList.Remove(s);
                        s.Cost = n.Cost + 1;
                        s.Parent = n;
                        addToDataStructor(s);

                    }
                }
                OpenListSize = openList.Count;
            }
            return null;
        }
    }
}