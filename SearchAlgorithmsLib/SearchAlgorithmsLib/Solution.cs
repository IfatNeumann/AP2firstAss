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
            solObj["Trace"] = Newtonsoft.Json.JsonConvert.SerializeObject(Trace);
            return solObj.ToString();
        }
    }
}
