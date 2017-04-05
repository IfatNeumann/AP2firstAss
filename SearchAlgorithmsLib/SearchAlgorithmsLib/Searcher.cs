using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        private MyPriorityQueue<State> openList;
        private intevaluatedNodes;
        publicSearcher()
        {
            openList = new MyPriorityQueue<State>();
            evaluatedNodes = 0;
        }
        protected State popOpenList()
        {
            evaluatedNodes++;
            return openList.poll();
        }
    }
}
