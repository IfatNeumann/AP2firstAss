using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Client1
{
    public class Progrm
    {
        static void Main(string[] args)
        {
            Client client = new Client(555,"127.0.0.1");
            client.Handle();

        }
    }
}
