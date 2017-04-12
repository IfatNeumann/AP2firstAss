using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace TcpClient
{
    public class Client
    {
        private int port;
        private string ip;

        public Client(int p, string i)
        {
            this.port = p;
            this.ip = i;
        }

        public void Handle()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), this.port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);

                Send sender = new Send(server);
                Task.Factory.StartNew(sender.Handle);

                while (true)
                {
                    byte[] data = new byte[1024];
                    int recv = server.Receive(data);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server." + e.ToString());
            }
        }
    }
}
