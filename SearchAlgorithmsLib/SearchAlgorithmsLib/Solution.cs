using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class of the solution
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        private int evaluatedNodes;
        /// <summary>
        /// Gets or sets the evaluated nodes.
        /// </summary>
        /// <value>
        /// The evaluated nodes.
        /// </value>
        public int EvaluatedNodes
        {
            get
            {
                return this.evaluatedNodes;
            }
            set
            {
                this.evaluatedNodes = value;
            }
        }
       
        /// <summary>
        /// The trace of the solution 
        /// </summary>
        private Queue<State<T>> trace = new Queue<State<T>>();
        
        /// <summary>
        /// Gets or sets the trace.
        /// </summary>
        /// <value>
        /// The trace.
        /// </value>
        public Queue<State<T>> Trace {
            get {
                return this.trace;
            }
            set {
                this.trace = value;
            }
        }
        
        /// <summary>
        /// convert to the json format.
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            JObject solObj = new JObject();
            solObj["Trace"] = Newtonsoft.Json.JsonConvert.SerializeObject(Trace);
            return solObj.ToString();
        }


    }
}
