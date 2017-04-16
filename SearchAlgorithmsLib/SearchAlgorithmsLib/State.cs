using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T> {//
        private T state; // the state represented by a string
        private double cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom; // the state we came from to this state (setter)
        public State(T state) // CTOR
        {
            this.state= state;
        }
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
        public double Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }
        public State<T> Parent
        {
            get { return this.cameFrom; }
            set { this.cameFrom = value; }
        }
        public T myState
        {
            get { return this.state; }
            set { this.state = value; }
        }
        public Solution<T> backTrace()
        {
            Solution<T> s = new Solution<T>();
            State<T> thisState = this;
            while (thisState.Parent != null)
            {
                s.Trace.Enqueue(thisState);
                thisState = thisState.Parent;
            }
            return s;
        }

        public static State<T> getState(T state)
        {
            return StatePool<T>.Instance.getState(state);
        }

        //singleton
        public sealed class StatePool<T>
        {
            private Dictionary<T,State<T>> pool;

            private StatePool()
            {
                this.pool = new Dictionary<T, State<T>>();
            }

            private static volatile StatePool<T> instance;

            private static object syncRoot = new object();

            public static StatePool<T> Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                            {
                                instance = new StatePool<T>();
                            }
                        }
                    }
                    return instance;
                }
            }

            public State<T> getState(T state)
            {
                if (!pool.ContainsKey(state))
                {
                    State<T> newState = new State<T>(state);
                    pool.Add(state, newState);
                } 
                return pool[state];
            }
        }
    }
}
