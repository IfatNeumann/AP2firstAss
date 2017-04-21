using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Configuration;


namespace Client1
{
    public class Progrm
    {
        static void Main(string[] args)
        {

            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"].ToString();
            Client client = new Client(port,ip);
            client.Handle();

        }
    }
}
