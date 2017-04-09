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
        private SimplePriorityQueue<State<T>> openList = new SimplePriorityQueue<State<T>>();
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
            bool inClosed = false, inOpenList = false ;
            addToDataStructor(searchable.getInitialState()); // inherited from Searcher
            int OpenListSize = openList.Count;
            while (OpenListSize > 0)
            {
                State<T> n = popDataStructor(); // inherited from Searcher, removes the best state
                Console.WriteLine("the point:" + n.myState.ToString());
                closed.Add(n);
                Console.Write("\t in closed: ");
                foreach (State<T> s1 in closed)
                {
                    Console.Write(s1.myState.ToString());
                }
                if (n.Equals(searchable.getGoalState()))
                    return n.backTrace(); // private method, back traces through the parents
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                Console.WriteLine("\n num of neighbors:" + succerssors.Count.ToString());
                foreach (State<T> s in succerssors)
                {
                    Console.WriteLine("neighbor:" + s.myState.ToString());
                    foreach (State<T> s1 in closed)
                    {
                        if (s.myState.Equals(s1.myState))
                        {
                            inClosed = true;
                            break;
                        }
                    }
                    foreach (State<T> s1 in openList)
                    {
                        if (s.myState.Equals(s1.myState))
                        {
                            inOpenList = true;
                            break;
                        }
                    }
                    if (!inClosed && !inOpenList)
                    {
                        s.Parent = n;// already done by getSuccessors
                        s.Cost = n.Cost + 1;
                        addToDataStructor(s);
                    }
                    else if (inOpenList || (n.Cost + 1 < s.Cost))//is inside the open list
                    {
                        openList.Remove(s);
                        s.Cost = n.Cost + 1;
                        s.Parent = n;
                        addToDataStructor(s);

                    }
                    inOpenList = false;
                    inClosed = false;
                }
                Console.Write("\n\t in open: ");
                foreach (State<T> s1 in openList)
                {
                    Console.Write(s1.myState.ToString());
                }
                OpenListSize = openList.Count;
                Console.WriteLine("\n open list size:" + openList.Count.ToString());
            }
            return null;
        }
    }
}