using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public class Play:ICommand
    {
        public Play(IModel model)
        {

        }
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            
        }
    }
}
