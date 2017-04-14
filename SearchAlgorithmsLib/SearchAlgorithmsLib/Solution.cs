using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        public Queue<State<T>> trace = new Queue<State<T>>();
        public Queue<State<T>> Trace { get; set; }
        public string ToJSON()
        {
            JObject solObj = new JObject();
            solObj["Trace"] = Trace;
            JObject startObj = new JObject();
            startObj["Row"] = InitialPos.Row;
            startObj["Col"] = InitialPos.Col;
            mazeObj["Start"] = startObj;
            //...
            return mazeObj.ToString();
        }
    }
}
