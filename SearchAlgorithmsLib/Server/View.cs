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
        private IClientHandler ch;        private TcpListener listener;

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


        public void StartConnection()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PortNum);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);

            newsock.Listen(10);
            Task task = new Task(() => {
                while (true)
                {
                    Socket client = newsock.Accept();
                    ch.HandleClient(client);
                    //PresenterForView presenter = new PresenterForView(this.controller);
                    //Recive receiver = new Recive(client, controller);
                    // Task.Factory.StartNew(receiver.Handle);
                }
            }
        }
    }
}