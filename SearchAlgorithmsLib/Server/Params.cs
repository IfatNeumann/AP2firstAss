using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public class Params
    {
        string param1;
        string param2;
        Socket client;


        public string Param1
        {
            get
            {
                return param1;
            }

            set
            {
                param1 = value;
            }
        }
        public string Param2
        {
            get
            {
                return param2;
            }

            set
            {
                param2 = value;
            }
        }
        public Socket Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }


        public Params(string p1, string p2, Socket c)
        {
            this.Param1 = p1;
            this.Param2 = p2;
            this.Client = c;
        }
    }
}

