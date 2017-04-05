using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private MyPriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public Searcher()
        {
            openList = new MyPriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.poll();
        }
        // the search method
        Solution search(ISearchable<T> searchable){
            Solution s = new Solution();
            return s;
        }
        // get how many nodes were evaluated by the algorithm
        int getNumberOfNodesEvaluated()
        {
            return 0;
        }
    }
}
