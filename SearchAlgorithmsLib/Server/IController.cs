using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    interface IController
    {
        string HandleRequest(string data, Socket client);
        IView View
        {
            get;
            set;
        }
        IModel Model
        {
            get;
            set;
        }
    }
}
