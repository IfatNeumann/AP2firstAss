using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public interface IController
    {
        IModel Model { get; set; }
        IClientHandler Ch { get; set; }
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
