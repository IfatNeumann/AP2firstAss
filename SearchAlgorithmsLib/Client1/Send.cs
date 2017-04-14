using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Client1
{
    public class Send
    {
        private Socket server;
        public Send(Socket s)
        {
            this.server = s;
        }

        public void Handle()
        {
            while (true)
            {
                string input = Console.ReadLine();
                server.Send(Encoding.ASCII.GetBytes(input));
            }
        }
    }
}