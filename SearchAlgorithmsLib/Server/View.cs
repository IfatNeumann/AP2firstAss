using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using TcpClient;


namespace Server
{
    public class View : IView
    {
        private int portNum;
        private IController controller;
        private IClientHandler ch;
        private TcpListener listener;


        public IController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
            }
        }

        public int PortNum
        {
            get
            {
                return portNum;
            }

            set
            {
                portNum = value;
            }
        }

        public View(int port, IClientHandler ch)
        {
            this.PortNum = port;
            this.ch = ch;
        }


        public void Start()
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNum);
        listener = new TcpListener(ep);

        listener.Start();
 Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }        public void Stop()
        {
            listener.Stop();
        }
    }
}