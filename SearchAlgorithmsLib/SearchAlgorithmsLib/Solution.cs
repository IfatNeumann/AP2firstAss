using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class of the solution
    /// </summary>
    /// <typeparam name="T">the generic type</typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// The trace of the solution 
        /// </summary>
        private Queue<State<T>> trace = new Queue<State<T>>();

        /// <summary>
        /// Gets and sets the evaluated nodes.
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
        /// Gets or sets the trace.
        /// </summary>
        /// <value>
        /// The trace.
        /// </value>
        public Queue<State<T>> Trace
        {
            get
            {
                return this.trace;
            }

            set
            {
                this.trace = value;
            }
        }

        /// <summary>
        /// convert to the JSON format.
        /// </summary>
        /// <returns>the JSON version of the solution</returns>
        public string ToJSON()
        {
            JObject solObj = new JObject();
            solObj["Trace"] = Newtonsoft.Json.JsonConvert.SerializeObject(Trace);
            return solObj.ToString();
        }
    }
}