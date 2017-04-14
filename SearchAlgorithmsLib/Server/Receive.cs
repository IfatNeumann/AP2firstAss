using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public class Receive
    {
        private Socket client;
        private byte[] data;
        private PresenterForView c1;

        public byte[] SetAndGetData
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public Receive(Socket client, PresenterForView p)
        {
            this.client = client;
            this.data = new byte[1024];
            this.c1 = p;
        }

        public void Handle()
        {
            c1.ChooseOption += this.UpdateReceiving;

            while (true)
            {
                byte[] biteMe = new byte[1024];
                int recv = client.Receive(biteMe);
                if (recv == 0)
                {
                    Console.WriteLine("error while receiving!");
                }

                c1.Message = biteMe;

                Task.Factory.StartNew(this.c1.Handle);
            }
        }

        public void UpdateReceiving(byte[] d)
        {
            string info = System.Text.Encoding.UTF8.GetString(d, 0, d.Length);
            int i = info.IndexOf('\0');
            if (i >= 0)
                info = info.Substring(0, i);
            string output = this.c1.UpdateReceive(info, client);

            d = new byte[1024];
            d = System.Text.Encoding.UTF8.GetBytes(output);

            // send to client
            this.client.Send(d, d.Count(), SocketFlags.None);
        }
    }
}
