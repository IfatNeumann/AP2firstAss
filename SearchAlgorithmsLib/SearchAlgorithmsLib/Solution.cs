using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        public State<T> goal;
        public Solution(State<T> goal){
            this.goal = goal;
        }
        public Queue<State<T>> trace;
    }
}
