using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientHandler ch = new View();
            MyServer server = new MyServer(555, ch);
            server.StartConnection();

        }
    }
}
