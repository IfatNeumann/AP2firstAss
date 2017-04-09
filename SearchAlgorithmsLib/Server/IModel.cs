using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    interface IModel
    {
        string HandleRequest(string option, Socket client);
        IController Controller
        {
            get;
            set;
        }
    }
}
