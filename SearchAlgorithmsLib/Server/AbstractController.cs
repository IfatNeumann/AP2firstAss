using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public abstract class AbstractController
    {
        protected IModel model;
        protected IView view;
        abstract public string ExecuteCommand(string commandLine, TcpClient client);
    }
}
