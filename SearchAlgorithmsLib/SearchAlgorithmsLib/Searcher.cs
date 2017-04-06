using System;
using Priority_Queue;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {   
        public int evaluatedNodes;
        public abstract void addToDataStructor(State<T> state);
        public Searcher()
        {
            evaluatedNodes = 0;
        }
        public abstract State<T> popDataStructor();
        // the search method
        public abstract Solution<T> search(ISearchable<T> searchable);
        // get how many nodes were evaluated by the algorithm
        public abstract int getNumberOfNodesEvaluated();
    }
}
