using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(Int32.Parse(ConfigurationManager.AppSettings["port"]), 
                ConfigurationManager.AppSettings["ip"]);
            client.Handle();
        }
    }
}
